using OneDance.Core.Database.Models;
using OneDance.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace OneDance.Core.Gameplay.Music
{
    public class MediaControl
    {
        private MediaPlayer m_player = new MediaPlayer();

        public MediaControl(List<MusicModel> songs)
        {
            Songs = new LinkedList<MusicModel>(songs);
            Randomize();

            m_player.Open(CurrentSong.Value.MediaUri);
            m_player.MediaEnded += StartNext;
            m_player.Volume = 100;
        }

        /// <summary>
        /// Música atual
        /// </summary>
        private LinkedListNode<MusicModel> CurrentSong
        {
            get;
            set;
        }

        /// <summary>
        /// Sequência de músicas
        /// </summary>
        private LinkedList<MusicModel> Songs
        {
            get;
            set;
        }

        /// <summary>
        /// Inicía a música
        /// </summary>
        public void Start()
        {
            if (!m_player.HasAudio)
                return;

            m_player.Play();
        }

        /// <summary>
        /// Pausa a música
        /// </summary>
        public void Pause()
        {
            if (!m_player.CanPause)
                return;

            m_player.Pause();
        }

        /// <summary>
        /// Finaliza a música, fechando o player
        /// </summary>
        public void Stop()
        {
            m_player.Close();

            if (!m_player.HasAudio)
                return;

            m_player.Stop();
        }

        /// <summary>
        /// Avança para a próxima música
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StartNext(object sender, EventArgs e)
        {
            Stop();

            if (CurrentSong.Next == null)
                CurrentSong = Songs.First;

            else
                CurrentSong = CurrentSong.Next;
            
            m_player.Open(CurrentSong.Value.MediaUri);
            Start();
        }

        /// <summary>
        /// Finaliza o player atual e randomiza a sequência de músicas existentes, criando uma nova lista
        /// </summary>
        public void Randomize()
        {
            Stop();

            var randomMusics = Songs.Shuffle();
            Songs = new LinkedList<MusicModel>(randomMusics);

            CurrentSong = Songs.First;
            m_player.Open(CurrentSong.Value.MediaUri);
        }

        /// <summary>
        /// Finaliza o gerenciamento de músicas
        /// </summary>
        public void Finalizar()
        {
            Stop();
            m_player.MediaEnded -= StartNext;
        }
    }
}
