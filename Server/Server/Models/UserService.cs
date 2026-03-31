using MySqlConnector;

public class UserService
{
    private readonly MySqlConnection _connection;

    public UserService(MySqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<string>> GetUsers()
    {
        var result = new List<string>();

        await _connection.OpenAsync();

        var cmd = new MySqlCommand("SELECT name FROM users", _connection);
        var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(reader.GetString(0));
        }

        return result;
    }
}