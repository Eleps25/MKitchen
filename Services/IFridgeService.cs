using MKitchen.Models;

namespace MKitchen.Services
{
    public interface IFridgeService
    {
        List<FridgeItem> GetFridgeItems();
        FridgeItem? GetFridgeItemById(int id);
        FridgeItem? GetSoonestExpiringActiveFridgeItem();
        List<FridgeItem> GetSoonestExpiringActiveFridgeItems(int itemsAmount);
        List<FridgeItem> GetExpiredFridgeItems();
        FridgeItem AddFridgeItem(FridgeItem item);
        bool UpdateFridgeItem(int id, FridgeItem item);
        bool DeleteFridgeItem(int id);
    }
}
