using Homework2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book(){ Author="Dostoevsky", Title="Zlostorstvo i kazna"},
            new Book(){ Author="Daniel defoe", Title="Robinson Crusoe" }
        };
    }
}
