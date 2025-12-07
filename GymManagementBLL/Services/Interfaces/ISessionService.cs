using GymManagementBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int sessionId);
        bool CreateSession(CreateSessionViewModel input);
        bool UpdateSession(int sessionId, UpdateSessionViewModel input);    
        bool RemoveSession(int sessionId);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
    }
}
