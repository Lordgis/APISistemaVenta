using System;

namespace SistemaVenta.BLL.Excepciones
{
    public class ReparacionNotFoundException : Exception
    {
        public ReparacionNotFoundException() { }

        public ReparacionNotFoundException(string message)
            : base(message) { }

        public ReparacionNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class ReparacionDeleteException : Exception
    {
        public ReparacionDeleteException() { }

        public ReparacionDeleteException(string message)
            : base(message) { }

        public ReparacionDeleteException(string message, Exception inner)
            : base(message, inner) { }
    } 
    
    public class ReparacionUpdateException : Exception
    {
        public ReparacionUpdateException() { }

        public ReparacionUpdateException(string message)
            : base(message) { }

        public ReparacionUpdateException(string message, Exception inner)
            : base(message, inner) { }
    }



}
