namespace SqlClient.Models;

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DateFrom { get; set; }
    public int DateTo { get; set; }
    public int MaxPeople { get; set; }
}
