namespace PlayingField
{
    public class PlayingFieldController
    {
        private readonly PlayingFieldView.Pool _playingFieldViewPool;
        private readonly PlayingFieldConfig _playingFieldConfig;

        public PlayingFieldController(
            PlayingFieldView.Pool playingFieldViewPool,
            PlayingFieldConfig playingFieldConfig)
        {
            _playingFieldViewPool = playingFieldViewPool;
            _playingFieldConfig = playingFieldConfig;
        }

        public PlayingFieldView SpawnPlayingVew(DiffcultLevel diffcultLevel)
        {
            var view = _playingFieldViewPool.Spawn(_playingFieldConfig.GetField(DiffcultLevel.Normal));
            return view;
        }
    }
}