using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Conductor.Devices.Interfaces.Capabilities;
using Conductor.Devices.Interfaces.Connectivity;
using Conductor.Devices.Interfaces.Exceptions;
using TIPC.DenonAvr;

namespace Conductor.Devices.Implementations.DenonAvr
{
    public class DenonAvrMainZone :
        INetworkDevice,
        IPower<DenonZonePowerState>,
        ISources<DenonInputSource>,
        IVolume<float?>,
        IMuting<DenonMuteState>,
        IAudioMode<DenonSurroundMode>
    {
        private readonly IDenonController _denonController;

        public DenonAvrMainZone(IDenonController denonController)
        {
            _denonController = denonController ?? throw new ArgumentNullException(nameof(denonController));
            
            Initialize();
        }

        private void Initialize()
        {
            _denonController.Connect();
        }

        public IDictionary<DenonInputSource, string> Sources { get; } = new Dictionary<DenonInputSource, string>
        {
            { DenonInputSource.Phono, "Phono" },
            { DenonInputSource.CD, "CD," },
            { DenonInputSource.Tuner, "Tuner" },
            { DenonInputSource.DVD, "DVD," },
            { DenonInputSource.BD, "Blu-Ray," },
            { DenonInputSource.TV, "TV," },
            { DenonInputSource.Sat, "Cable / Sat," },
            { DenonInputSource.Game, "Game" },
            { DenonInputSource.MPlay, "Media Player" },
            { DenonInputSource.Aux1, "Auxiliary 1" },
            { DenonInputSource.Aux2, "Auxiliary 2" },
            { DenonInputSource.Net, "Network" }
        };

        public Task<DenonInputSource?> GetActiveSource(CancellationToken cancellationToken = default) =>
            _denonController.ZoneInputSelect.GetStateAsync(DenonZone.ZM, cancellationToken);

        public Task<DenonInputSource?>  SetSource(DenonInputSource source, CancellationToken cancellationToken = default) =>
            _denonController.ZoneInputSelect.SetStateAsync(DenonZone.ZM, source, cancellationToken);

        public IDictionary<DenonZonePowerState, string> PowerStatuses { get; } = new Dictionary<DenonZonePowerState, string>
        {
            { DenonZonePowerState.On, "On" },
            { DenonZonePowerState.Off, "Off" }
        };

        public Task<DenonZonePowerState?> GetPowerStatus(
            CancellationToken cancellationToken = default) =>
            _denonController.ZonePower.GetStateAsync(DenonZone.ZM, cancellationToken);

        public Task<DenonZonePowerState?> PowerOn(CancellationToken cancellationToken = default) => 
            SwitchPower(DenonZonePowerState.On, cancellationToken);

        public Task<DenonZonePowerState?> PowerOff(CancellationToken cancellationToken = default) => 
            SwitchPower(DenonZonePowerState.Off, cancellationToken);

        public async Task<DenonZonePowerState?> SwitchPower(
            DenonZonePowerState status,
            CancellationToken cancellationToken = default) =>
            await _denonController.ZonePower.SetStateAsync(DenonZone.ZM, status, cancellationToken);

        public async Task<DenonZonePowerState?> PowerToggleAsync(
            CancellationToken cancellationToken = default) =>
            await GetPowerStatus(cancellationToken) switch
            {
                DenonZonePowerState.Off => await SwitchPower(DenonZonePowerState.On, cancellationToken),
                DenonZonePowerState.On => await SwitchPower(DenonZonePowerState.Off, cancellationToken),
                null => null,
                _ => throw new PowerStatusException()
            };

        public Task<float?> GetVolume(CancellationToken cancellationToken = default) =>
            _denonController.ZoneVolume.GetStateAsync(DenonZone.ZM, cancellationToken);

        public Task<float?> IncreaseVolume(CancellationToken cancellationToken = default) =>
            _denonController.ZoneVolume.SetUpAsync(DenonZone.ZM, cancellationToken);

        public Task<float?> DecreaseVolume(CancellationToken cancellationToken = default) =>
            _denonController.ZoneVolume.SetDownAsync(DenonZone.ZM, cancellationToken);

        public async Task<float?> SetVolume(float? volume, CancellationToken cancellationToken = default) =>
            volume.HasValue
            ? await _denonController.ZoneVolume.SetAbsoluteAsync(DenonZone.ZM, volume.Value, cancellationToken)
            : null;

        public IDictionary<DenonMuteState, string> MutingStatuses { get; } = new Dictionary<DenonMuteState, string>
        {
            { DenonMuteState.Off, "Unmuted" },
            { DenonMuteState.On, "Muted" }
        };
        
        public Task<DenonMuteState?> GetMutingStatus(CancellationToken cancellationToken = default) =>
            _denonController.ZoneMuting.GetStateAsync(DenonZone.ZM, cancellationToken);

        public Task<DenonMuteState?> Mute(CancellationToken cancellationToken = default) => 
            SwitchMuting(DenonMuteState.On, cancellationToken);

        public Task<DenonMuteState?> Unmute(CancellationToken cancellationToken = default) => 
            SwitchMuting(DenonMuteState.Off, cancellationToken);

        public Task<DenonMuteState?> SwitchMuting(DenonMuteState status, CancellationToken cancellationToken = default) =>
            _denonController.ZoneMuting.SetStateAsync(DenonZone.ZM, status, cancellationToken);

        public async Task<DenonMuteState?> MutingToggleAsync(CancellationToken cancellationToken = default) =>
            await GetMutingStatus(cancellationToken) switch
            {
                DenonMuteState.Off => await SwitchMuting(DenonMuteState.On, cancellationToken),
                DenonMuteState.On => await SwitchMuting(DenonMuteState.Off, cancellationToken),
                null => null,
                _ => throw new MutingStatusException()
            };

        public IDictionary<DenonSurroundMode, string> AudioModes { get; } = new Dictionary<DenonSurroundMode, string>
        {
            { DenonSurroundMode.Direct, "Direct" },
            { DenonSurroundMode.Pure_Direct, "Pure Direct" },
            { DenonSurroundMode.Stereo, "Stereo" },
            { DenonSurroundMode.Auto, "Auto" },
            { DenonSurroundMode.Dolby_Digital, "Dolby Digital" },
            { DenonSurroundMode.DTS_Surround, "DTS" },
            { DenonSurroundMode.MCH_Stereo, "Multichannel Stereo" },
            { DenonSurroundMode.Wide_Screen, "Widescreen" },
            { DenonSurroundMode.Super_Stadium, "Super Stadium" },
            { DenonSurroundMode.Rock_Arena, "Rock Arena" },
            { DenonSurroundMode.Jazz_Club, "Jazz Club" },
            { DenonSurroundMode.Classic_Concert, "Classic Concert" },
            { DenonSurroundMode.Mono_Movie, "Mono Movie" },
            { DenonSurroundMode.Matrix, "Matrix" },
            { DenonSurroundMode.Video_Game, "Video Game" },
            { DenonSurroundMode.Virtual, "Virtual" }
        };

        public Task<DenonSurroundMode?> GetActiveAudioMode(CancellationToken cancellationToken = default) =>
            _denonController.SurroundMode.GetStateAsync(cancellationToken);

        public Task<DenonSurroundMode?> SetAudioMode(DenonSurroundMode audioMode, CancellationToken cancellationToken = default) =>
            _denonController.SurroundMode.SetStateAsync(audioMode, cancellationToken);
    }
}