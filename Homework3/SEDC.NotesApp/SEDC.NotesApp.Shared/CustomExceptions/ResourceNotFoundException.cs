using System;

namespace SEDC.NotesApp.Shared.CustomExceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(int id, string message) : base(message) 
        {
            //log id
        }
    }
}
