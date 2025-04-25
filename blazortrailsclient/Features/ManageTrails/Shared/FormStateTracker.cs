using blazortrailsclient.State;
using blazortrailsshared.Features.ManageTrails.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace blazortrailsclient.Features.ManageTrails.Shared
{
    public class FormStateTracker : ComponentBase
    {
        [Inject]
        public AppState AppState { get;set; }
        [CascadingParameter]
        private EditContext CascadingEditContext { get; set; }

        protected override void OnInitialized()
        {
            if(CascadingEditContext is null)
            {
                throw new InvalidOperationException(
                    $"{nameof(CascadingEditContext)} is required for parameter type fo {nameof(EditContext)}");
            }
            CascadingEditContext.OnFieldChanged += CascadingEditContext_OnFiledChanged;
        }

        private void CascadingEditContext_OnFiledChanged(object sender,FieldChangedEventArgs e)
        {
            var trail = e.FieldIdentifier.Model as TrailDto;
            Console.WriteLine($"Field changed: {e.FieldIdentifier.FieldName}");
            if (trail.Id == 0)
            {
                AppState.SetUnSavedNewTrailDto(trail);
            }
        }
    }
}
