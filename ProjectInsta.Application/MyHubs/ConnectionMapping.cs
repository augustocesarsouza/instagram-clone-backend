using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectInsta.Application.MyHubs
{
    public class ConnectionMapping
    {
        private readonly Dictionary<string, string> _connections = new Dictionary<string, string>();
        
        public void Add(string key, string connectionId)
        {
            lock(_connections)
            {
                string connection;
                if(!_connections.TryGetValue(key, out connection)){
                    _connections.Add(key, connectionId);
                }
                else
                {
                    _connections[key] = connectionId;
                }
            }
        }

        public string GetConnection(string key)
        {
            string connection;
            if (_connections.TryGetValue(key, out connection))
            {
                return connection;
            }

            return "";
        }

        public void Remove(string key) 
        { 
            lock(_connections) 
            {
                string connections;
                if(_connections.ContainsKey(key))
                {
                    _connections.Remove(key);
                }
            }
        
        }
    }
}
