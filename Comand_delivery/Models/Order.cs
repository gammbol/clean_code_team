// Модель заказа
// Хранит информацию о заказе
public class Order
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public string Status { get; set; } = "New"; // Новый, Готовится, Доставляется, Завершён, Отменён

    public Order(int id, string customerName, string address, decimal totalAmount)
    {
        Id = id;
        CustomerName = customerName;
        Address = address;
        TotalAmount = totalAmount;
    }

    public override string ToString()
    {
        return $"Заказ #{Id} | {CustomerName} | {Address} | {TotalAmount:C} | Статус: {Status}";
    }
}