using Blazored.LocalStorage;
using blazortrailsclient.Features.Home.Model;

namespace blazortrailsclient.State
{
    public class FavoriteTrailState
    {
        private const string FavouriteTrailsKey = "favtrails";
        private bool _isInitialized { get; set; }
        private List<TrailModel> _favouriteTrails = new();
        private readonly ILocalStorageService localStorageService;
        public IReadOnlyList<TrailModel> FavouriteTrails  => _favouriteTrails.AsReadOnly();
        public event Action? OnChange;

        public FavoriteTrailState(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task OnInitialized()
        {
            if (!_isInitialized)
            {
                _favouriteTrails = await localStorageService.GetItemAsync<List<TrailModel>>(FavouriteTrailsKey)?? new List<TrailModel>();
                _isInitialized = true;
                NotifyStateHasChanged();
            }
        }

        public async Task AddFavourite(TrailModel trailModel)
        {
            if (_favouriteTrails.Any(f => f.Id == trailModel.Id)) return;
            _favouriteTrails.Add(trailModel);
            await localStorageService.SetItemAsync(FavouriteTrailsKey, _favouriteTrails);
            NotifyStateHasChanged();
        }

        public async Task RemoveFavourite(TrailModel trailModel)
        {
            if(!_favouriteTrails.Any(f=>f.Id == trailModel.Id)) return;
            _favouriteTrails.Remove(trailModel);
            await localStorageService.SetItemAsync(FavouriteTrailsKey, _favouriteTrails);
            NotifyStateHasChanged();
        }

        public bool IsFavourite(TrailModel trailModel)
        {
            if(_favouriteTrails.Any(f => f.Id == trailModel.Id))
            {
                return true;
            }
            return false;
        }

        private void NotifyStateHasChanged()
        {
            OnChange?.Invoke();
        }
    }
}
