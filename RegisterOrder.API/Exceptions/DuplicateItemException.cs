namespace RegisterOrder.API.Exceptions;

public class DuplicateItemException(string message) : Exception(message);
