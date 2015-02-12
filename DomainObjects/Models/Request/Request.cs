using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Models.Request
{
    /// <summary>
    /// Request object
    /// </summary>
    public class Request
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
    /// <summary>
    /// Request Object with generic T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Request<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public T Obj { get; set; }
        public int TotalCount { get; set; }
    }
    public class RequestList<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IList<T> Obj { get; set; }
        public int TotalCount { get; set; }
    }
}
