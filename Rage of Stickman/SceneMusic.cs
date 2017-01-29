using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Rage_of_Stickman
{
	class SceneMusic : SceneComponent
	{
		private Song backgroundmusic;
		private bool isPlaying;

		public SceneMusic(Song backgroundmusic, bool active = true)
			: base(null, Vector2.Zero, Vector2.One, active, false)
		{
			MediaPlayer.Stop();
			this.backgroundmusic = backgroundmusic;
			MediaPlayer.IsRepeating = true;
			isPlaying = false;
		}

		public override void Update()
		{
			base.Update();

			if (active)
			{
				if (!isPlaying && backgroundmusic != null)
				{
					MediaPlayer.Play(backgroundmusic);
					isPlaying = true;
				}
			}
			else
			{
				MediaPlayer.Stop();
				isPlaying = false;
			}
		}
	}
}
