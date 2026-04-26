namespace RegisterOrder.API.Exceptions;

public class OrderNotFoundException(int id) : Exception($"Pedido {id} não encontrado.");
