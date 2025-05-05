namespace SqlClient.Exceptions;

public class NotFoundException(string message) : Exception(message);
