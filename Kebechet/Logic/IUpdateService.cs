using System;

namespace Kebechet.Logic
{
    public interface IUpdateService
    {
        UpdateResult UpdateHost(string host, string ipAddress);
    }
}