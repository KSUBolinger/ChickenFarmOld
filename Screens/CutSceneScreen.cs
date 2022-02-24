﻿/*
    This screen is an adapted version of the one provided by Nathan Bean in the Game Architecture Tutorial to suit this game
*/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using GameProject0.StateManagement;
using Microsoft.Xna.Framework.Input;

namespace GameProject0.Screens
{
    public class CutSceneScreen : GameScreen
    {
        ContentManager _content;
        Video _video;
        VideoPlayer _player;
        bool _isPlaying = false;
        InputAction _skip;

        public CutSceneScreen()
        {
            _player = new VideoPlayer();
            _skip = new InputAction(new Buttons[] { Buttons.A }, new Keys[] { Keys.Space, Keys.Enter }, true);
        }

        public override void Activate()
        {
            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
                _player = new VideoPlayer();

            }
            _video = _content.Load<Video>("liftoff_of_smap");

        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {

            if (!_isPlaying)
            {
                _player.Play(_video);
                _isPlaying = true;
            }
            PlayerIndex player;
            if (_skip.Occurred(input, null, out player))
            {
                _player.Stop();
                ExitScreen();
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (_player.PlayPosition >= _video.Duration) ExitScreen();
        }

        public override void Deactivate()
        {
            _player.Stop();
            _isPlaying = false;
        }

        public override void Draw(GameTime gameTime)
        {
            if (_isPlaying)
            {
                ScreenManager.SpriteBatch.Begin();
                ScreenManager.SpriteBatch.Draw(_player.GetTexture(), Vector2.Zero, Color.White);
                ScreenManager.SpriteBatch.End();
            }
        }
    }
}