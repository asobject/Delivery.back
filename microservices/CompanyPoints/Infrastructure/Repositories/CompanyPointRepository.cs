
using BuildingBlocks.Enums;
using BuildingBlocks.Models;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.DTOs;

namespace Infrastructure.Repositories;

public class CompanyPointRepository(ApplicationDbContext context) : Repository<CompanyPoint>(context), ICompanyPointRepository
{
    public async Task<string?> GetExistingPointName(CompanyPoint point, int? id = null)
    {
        string sqlQuery = @"
 SELECT *
 FROM public.""CompanyPoints"" p
 WHERE (p.""Name"" = {0} OR p.""PointType"" = {1})
 AND ST_Distance(p.""Coordinates"", ST_MakePoint({2}, {3}, 4326)::geography) < 0.0001";
        if (id is not null)
        {
            sqlQuery += @" AND p.""Id"" != {4}";
        }

        var existingPoints = await FindBySqlAsync(sqlQuery,
            point.Name, point.PointType, point.Coordinates.Longitude, point.Coordinates.Latitude, id!);

        return existingPoints.FirstOrDefault()?.Name;
    }
    public async Task<IEnumerable<CompanyPoint>> GetCompanyPointsInRadiusAsync(GeoPoint center, double radiusKm)
    {
        return await _dbSet
            .FromSqlInterpolated($@"
                SELECT * FROM ""Points"" 
                WHERE ST_DWithin(
                    ""Coordinates""::geography, 
                    ST_SetSRID(ST_MakePoint({center.Longitude}, {center.Latitude}), 4326)::geography,
                    {radiusKm * 1000}
                )")
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ClusterDTO>> GetClustersAsync(params PointStatus[] statuses)
    {
        var query = _dbSet.AsQueryable();

        if (statuses.Length > 0)
        {
            query = query.Where(p => statuses.Contains(p.PointStatus));
        }

        return await query
            .Select(p => new ClusterDTO
            {
                Id = p.Id,
                Coordinates = p.Coordinates
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public  async Task<PagedResultDTO<CompanyPoint>> GetPagedAsync(
    int pageNumber,
    int pageSize,
    Expression<Func<CompanyPoint, bool>>? predicate = null)
    {
        // Начинаем с базового запроса
        IQueryable<CompanyPoint> baseQuery = _dbSet.AsQueryable();

        // Применяем фильтр, если он задан
        if (predicate != null)
        {
            baseQuery = baseQuery.Where(predicate);
        }

        // Вычисляем общее количество записей по базовому запросу
        int totalRecords = await baseQuery.CountAsync();

        // Применяем функции инклюдов для загрузки связанных сущностей
        IQueryable<CompanyPoint> query = baseQuery;

        query = query
              .Include(p => p.WorkingHours)
              .Include(p => p.Contacts)
              .Include(p => p.Locality)
              .ThenInclude(l => l.Province)
              .ThenInclude(n => n.Country);

        // Применяем пагинацию
        var data = await query
            .OrderBy(o=>o.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        // Возвращаем результат в виде PagedResultDTO<T>
        return new PagedResultDTO<CompanyPoint>
        {
            Data = data,
            TotalRecords = totalRecords
        };
    }
}