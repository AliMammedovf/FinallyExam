using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCode.Business.Exceptions;

public class FileNotExsistException : Exception
{
    public FileNotExsistException(string? message) : base(message)
    {
    }
}
