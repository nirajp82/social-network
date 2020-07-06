using SocialNetwork.DataModel;
using SocialNetwork.Dto;
using SocialNetwork.EF.Repo;
using SocialNetwork.Nucleus.Interfaces;
using SocialNetwork.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Nucleus
{
    internal class UserActivityHelper : IUserActivityHelper
    {
        #region Members
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapperHelper _mapperHelper;
        #endregion


        #region Constuctor
        public UserActivityHelper(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapperHelper mapperHelper)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
            _mapperHelper = mapperHelper;
        }
        #endregion


        #region Methods
        public async Task<IEnumerable<ActivityDto>> PrepareActivities(IEnumerable<Activity> dbActivities)
        {
            return await Prepare(dbActivities);
        }

        public async Task<ActivityDto> PrepareActivity(Activity dbActivity)
        {
            if (dbActivity != null)
            {
                var result = await Prepare(new List<Activity> { dbActivity });
                return result.FirstOrDefault();
            }
            return null;
        }
        #endregion


        #region Private Methods
        private async Task<IEnumerable<ActivityDto>> Prepare(IEnumerable<Activity> dbActivities)
        {
            IEnumerable<ActivityDto> activities = _mapperHelper.MapList<Activity, ActivityDto>(dbActivities);
            if (activities?.Any() == true)
            {
                var attendees = activities.SelectMany(a => a.Attendees.Select(a => a.AppUserId)).Distinct();
                IEnumerable<UserFollower> followingList = await _unitOfWork
                    .UserFollowerRepo
                    .FindAsync(u => attendees.Contains(u.UserId) && u.FollowerId == _userAccessor.GetCurrentUserId());

                foreach (var activity in activities)
                {
                    foreach (var attendee in activity.Attendees)
                    {
                        attendee.Following = followingList.Any(u => u.UserId == attendee.AppUserId);
                    }
                }
            }
            return activities;
        }
        #endregion
    }
}
