using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySqlX.XDevAPI.Common;
using System.Globalization;

namespace TrackSense.API.Entities.Interfaces
{
    public interface IDepotUsers
    {
        bool UserExist(string p_userLogin);
        //IEnumerable<User> GetAllUsers();
        User? GetUserByUserLogin(string p_userLogin);
        void UpdateUser(User p_user);
        void DeleteUser(string p_userLogin);
        void AddUser(User p_user);
        bool GetAvailabilityUserLogin(string p_userLogin);
        bool CheckUserToken(string p_token);
        UserToken? ConnectUser(string p_userLogin, string p_userPassword);
        void DeleteUserToken(UserToken p_userToken);
        void DeleteAllUserTokens(string p_userLogin);
        bool CheckUserPassword(string p_userLogin, string p_userPassword);
        void UpdateUserPassword(string p_userLogin, string p_oldPassword, string p_newPassword);
        IEnumerable<UserTrackSense>? GetAllUserTrackSenses(string p_userLogin);
        UserTrackSense? GetUserTrackSense(string p_userLogin);
        void AddUserTrackSense(UserTrackSense p_trackSense, string p_userLogin);
        void UpdateUserTrackSense(UserTrackSense p_userTrackSense);
        void DeleteUserTrackSense(string p_trackSenseId, string p_userLogin);
        UserStatistics? GetUserStatistics(string p_userLogin);
        IEnumerable<UserContact> GetUserContacts(string p_userLogin);
        UserContact? GetUserContactById(int p_userLogin);
        void AddUserContact(UserContact p_userContact, string p_userLogin);
        void UpdateUserContact(UserContact p_UserContact);
        void DeleteUserContact(int p_contactId);
        IEnumerable<UserInterestPoint> GetUserIntersetPoints(string p_userLogin);
        UserInterestPoint? GetUserIntersetPointById(int p_interestPointId);
        void AddUserInterestPoint(UserInterestPoint p_interestPoint, string p_userLogin);
        void UpdateUserInterestPoint(UserInterestPoint p_intersetPoint);
        void DeleteUserInterestPoint(int p_intersetPointId);
        IEnumerable<UserCompletedRide> GetUserCompletedRides(string p_userLogin);
        IEnumerable<UserPlannedRide> GetUserPlannedRides(string p_userLogin);
    }
}
