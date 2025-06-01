using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baristasyon.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<string> AskQuestionAsync(string question);
    }

}
