using MKitchen.Models;

namespace MKitchen.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly List<FridgeItem> _fridgeItemsList = new ();
        private int nextId = 1;

        public FridgeItem AddFridgeItem(FridgeItem item)
        {
            item.Id = nextId++;
            _fridgeItemsList.Add (item);
            return item;
        }

        public bool DeleteFridgeItem(int id)
        {
            var existingItem = GetFridgeItemById(id);

            if (existingItem is null) return false;
            _fridgeItemsList.Remove(existingItem);
            return true;
        }

        public FridgeItem? GetFridgeItemById(int id) => _fridgeItemsList.FirstOrDefault(item => item.Id == id);

        public List<FridgeItem> GetFridgeItems() => _fridgeItemsList;

        public bool UpdateFridgeItem(int id, FridgeItem item)
        {
            var existingItem = GetFridgeItemById(id);

            if (existingItem is null) return false;

            existingItem.Name = item.Name;
            existingItem.Quantity = item.Quantity;
            existingItem.ExpirationDate = item.ExpirationDate;
            return true;
        }

        private IEnumerable<FridgeItem> GetUnexpiredFridgeItemsSortedByExpiration()
        {
            return _fridgeItemsList
                .Where(item => item.ExpirationDate >= DateOnly.FromDateTime(DateTime.Today))
                .OrderBy(item => item.ExpirationDate);
        }

        public FridgeItem? GetSoonestExpiringActiveFridgeItem() => GetUnexpiredFridgeItemsSortedByExpiration().FirstOrDefault();
        public List<FridgeItem> GetSoonestExpiringActiveFridgeItems(int FridgeItemsAmount) => GetUnexpiredFridgeItemsSortedByExpiration().Take(FridgeItemsAmount).ToList();
        public List<FridgeItem> GetExpiredFridgeItems() => _fridgeItemsList.Where(item => item.ExpirationDate < DateOnly.FromDateTime(DateTime.Today)).ToList();
    }
}
