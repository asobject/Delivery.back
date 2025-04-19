

namespace Domain.DTOs;

public record OrderPointChangeDTO(Guid Tracker,int PointId,string Address,DateTime ChangeAt);