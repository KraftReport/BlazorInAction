using Blazored.LocalStorage;
using blazortrailsshared.Features.ManageTrails.Shared;

namespace blazortrailsclient.State
{
    public class AppState
    {
        public NewTrailState NewtrailState { get; }
        public FavoriteTrailState FavoriteTrailState { get; }
        public bool IsInitialized;

        public AppState(ILocalStorageService localStorageService)
        {
            FavoriteTrailState = new FavoriteTrailState(localStorageService);
            NewtrailState = new NewTrailState();
        }

        public async Task Initialize()
        {
            if (!IsInitialized)
            {
                await FavoriteTrailState.OnInitialized();
                IsInitialized = true;
            }
        }

        public class NewTrailState
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
}
