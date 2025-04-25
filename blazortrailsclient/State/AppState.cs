using blazortrailsshared.Features.ManageTrails.Shared;

namespace blazortrailsclient.State
{
    public class AppState
    {
        public TrailDto _unSavedNewTrailDto = new TrailDto();

        public void SetUnSavedNewTrailDto(TrailDto trailDto)
        {
            _unSavedNewTrailDto = trailDto;
        }

        public TrailDto GetUnSavedTrailDto()
        {
            return _unSavedNewTrailDto;
        }

        public void ClearUnSavedTrailDto()
        {
            _unSavedNewTrailDto = new TrailDto();
        }
    }
}
