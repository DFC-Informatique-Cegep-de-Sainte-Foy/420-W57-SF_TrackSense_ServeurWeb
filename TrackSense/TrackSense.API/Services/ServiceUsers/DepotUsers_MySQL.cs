using Microsoft.EntityFrameworkCore;
using TrackSense.API.Data;
using TrackSense.API.Entities;
using TrackSense.API.Entities.Interfaces;

namespace TrackSense.API.Services.ServiceUsers
{
    public class DepotUsers_MySQL : IDepotUsers
    {
        private readonly TracksenseContext m_context;

        public DepotUsers_MySQL(TracksenseContext tracksenseContext)
        {
            this.m_context = tracksenseContext;   
        }
        public void AddUser(User p_user)
        {
            if (p_user == null)
            {
                throw new ArgumentNullException(nameof(p_user));
            }
            try
            {
                DTOs.User user = new DTOs.User(p_user);
                m_context.Users.Add(user);
                m_context.SaveChanges();
                m_context.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
           
        }

        public void AddUserContact(UserContact p_userContact, string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public void AddUserInterestPoint(UserInterestPoint p_interestPoint, string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public void AddUserTrackSense(UserTrackSense p_trackSense, string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserPassword(string p_userLogin, string p_userPassword)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserToken(string p_token)
        {
            throw new NotImplementedException();
        }

        public UserToken? ConnectUser(string p_userLogin, string p_userPassword)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllUserTokens(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserContact(int p_contactId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserInterestPoint(int p_intersetPointId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserToken(UserToken p_userToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserTrackSense(string p_trackSenseId, string p_userLogin)
        {
            throw new NotImplementedException();
        }

       /* public IEnumerable<User> GetAllUsers()
        {
            return m_context.Users.f
        }*/

        public IEnumerable<UserTrackSense>? GetAllUserTrackSenses(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public bool GetAvailabilityUserLogin(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByUserLogin(string p_userLogin)
        {
            DTOs.User ?user = m_context.Users.Where(u => u.UserLogin ==p_userLogin)
                                             .Include(u=>u.Credential)
                                             .FirstOrDefault();
            return user?.ToEntity();
        }

        public IEnumerable<UserCompletedRide> GetUserCompletedRides(string p_userLogin)
        {
            List<UserCompletedRide> userCompletedRides = new List<UserCompletedRide>() ;
            try
            {
                userCompletedRides = m_context.UserCompletedRides
                                                    .Where(ucr => ucr.UserLogin == p_userLogin)
                                                    .Select(ucr => ucr.ToEntity())
                                                    .ToList();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message );
            }
            
            return userCompletedRides;
                
        }

        public IEnumerable<UserPlannedRide> GetUserPlannedRides(string p_userLogin)
        {
            List<UserPlannedRide> userPlannedRides = new List<UserPlannedRide>();
            try
            {
                userPlannedRides = m_context.UserPlannedRides
                                                    .Where(upr => upr.UserLogin == p_userLogin)
                                                    .Select(upr => upr.ToEntity())
                                                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return userPlannedRides;

        }

        public UserContact? GetUserContactById(int p_userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserContact> GetUserContacts(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public UserInterestPoint? GetUserIntersetPointById(int p_interestPointId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserInterestPoint> GetUserIntersetPoints(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public UserStatistics? GetUserStatistics(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public UserTrackSense? GetUserTrackSense(string p_userLogin)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User p_user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserContact(UserContact p_UserContact)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserInterestPoint(UserInterestPoint p_intersetPoint)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserPassword(string p_userLogin, string p_oldPassword, string p_newPassword)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserTrackSense(UserTrackSense p_userTrackSense)
        {
            throw new NotImplementedException();
        }
        public bool UserExist(string p_UserLogin)
        {
            return m_context.Users.Any(u => u.UserLogin == p_UserLogin);
        }

    }
}
