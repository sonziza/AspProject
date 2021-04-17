using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspProject.Interfaces.TestAPI
{
    /// <summary>
    /// Интерфейс для управления работой клиента с API
    /// Желательно, чтобы названия методов не перекликались с контроллером API
    /// </summary>
    interface IValuesClientService
    {
        IEnumerable<string> Get();

        string Get(int id);

        Uri Create(string value);

        HttpStatusCode Edit(int id, string value);

        bool Remove(int id);
    }
}
