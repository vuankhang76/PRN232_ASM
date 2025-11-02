using BusinessObjects;
using Repositories;

namespace Services;

/// <summary>
/// RubricItem service implementation for business logic
/// </summary>
public class RubricItemService : IRubricItemService
{
    private readonly IUnitOfWork _unitOfWork;

  public RubricItemService(IUnitOfWork unitOfWork)
    {
   _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RubricItem>> GetAllRubricItemsAsync()
 {
return await _unitOfWork.RubricItemRepository.GetAllAsync();
    }

 public async Task<RubricItem?> GetRubricItemByIdAsync(long rubricItemId)
    {
        return await _unitOfWork.RubricItemRepository.GetByIdAsync(rubricItemId);
    }

  public async Task<IEnumerable<RubricItem>> GetRubricItemsByExamIdAsync(int examId)
    {
  return await _unitOfWork.RubricItemRepository.GetByExamIdAsync(examId);
 }

    public async Task<RubricItem> CreateRubricItemAsync(int examId, string code, string title, decimal maxPoint,
      string? keywords = null, int? orderNo = null)
    {
    var rubricItem = new RubricItem
{
       ExamId = examId,
      Code = code,
      Title = title,
MaxPoint = maxPoint,
    Keywords = keywords,
        OrderNo = orderNo ?? 0
        };

        await _unitOfWork.RubricItemRepository.AddAsync(rubricItem);
   await _unitOfWork.SaveChangesAsync();

  return rubricItem;
  }

    public async Task<RubricItem> UpdateRubricItemAsync(long rubricItemId, string title, decimal maxPoint,
    string? keywords = null, int? orderNo = null)
    {
  var rubricItem = await _unitOfWork.RubricItemRepository.GetByIdAsync(rubricItemId);

  if (rubricItem == null)
   throw new InvalidOperationException($"RubricItem with ID '{rubricItemId}' not found.");

  rubricItem.Title = title;
     rubricItem.MaxPoint = maxPoint;
  rubricItem.Keywords = keywords;
      if (orderNo.HasValue)
        rubricItem.OrderNo = orderNo.Value;

    _unitOfWork.RubricItemRepository.Update(rubricItem);
    await _unitOfWork.SaveChangesAsync();

        return rubricItem;
}

    public async Task<bool> DeleteRubricItemAsync(long rubricItemId)
  {
 var rubricItem = await _unitOfWork.RubricItemRepository.GetByIdAsync(rubricItemId);

    if (rubricItem == null)
       return false;

  _unitOfWork.RubricItemRepository.Delete(rubricItem);
        return await _unitOfWork.SaveChangesAsync() > 0;
  }
}
