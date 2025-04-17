

namespace Domain.DTOs;

public record OrderPointChangeDTO(int OrderId,int PointId,string Address,DateTime ChangeAt);