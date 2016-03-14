using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Lync.Model.Conversation;
using System.IO;
using Microsoft.Lync.Model;
using SkyForBusLH.Properties;

namespace SkyForBusLH
{
    public partial class MainForm : Form
    {
        static readonly Dictionary<Conversation, ConversationContainer> ActiveConversations =
            new Dictionary<Conversation, ConversationContainer>();
        static Self _myself;
        static readonly string MyDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private const string ProgramFolder = "LyncIMHistory";
        private const string AllInOneFileName = @"AllLyncIMHistory.txt";

        public MainForm()
        {
            InitializeComponent();
            ConnectAndPrepare();
        }

        private delegate void SetText(string text);

        private void Main_Resize(object sender, EventArgs e)
        {
            notifyIcon.Visible = WindowState == FormWindowState.Minimized;
            ShowInTaskbar = WindowState != FormWindowState.Minimized;
        }

        private ConversationIdStorage _idStorage;
        public bool ConnectAndPrepare()
        {
            EnsureDirectoryExists(Path.Combine(MyDocsPath, ProgramFolder));
            EnsureDirectoryExists(Path.Combine(AppDataPath + ProgramFolder));
            _idStorage = new ConversationIdStorage(AppDataPath + ProgramFolder);

            LyncClient client = GetLyncClient();
            if (client == null)
            {
                return false;
            }
            _myself = client.Self;


            client.ConversationManager.ConversationAdded += ConversationManager_ConversationAdded;
            client.ConversationManager.ConversationRemoved += ConversationManager_ConversationRemoved;
            ConsoleWriteLine(Resources.Ready);
            return true;
        }

        private void ConversationManager_ConversationAdded(object sender, ConversationManagerEventArgs e)
        {
            var newcontainer = new ConversationContainer
            {
                Conversation = e.Conversation,
                ConversationCreated = DateTime.Now,
                ConversationId = _idStorage.Id++
            };

            ActiveConversations.Add(e.Conversation, newcontainer);
            e.Conversation.ParticipantAdded += Conversation_ParticipantAdded;
            e.Conversation.ParticipantRemoved += Conversation_ParticipantRemoved;
        }

        private void ConversationManager_ConversationRemoved(object sender, ConversationManagerEventArgs e)
        {
            e.Conversation.ParticipantAdded -= Conversation_ParticipantAdded;
            e.Conversation.ParticipantRemoved -= Conversation_ParticipantRemoved;
            ActiveConversations.Remove(e.Conversation);
        }

        void Conversation_ParticipantAdded(object sender, ParticipantCollectionChangedEventArgs args)
        {
            ((InstantMessageModality)args.Participant.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived += InstantMessageModality_InstantMessageReceived;
        }

        void Conversation_ParticipantRemoved(object sender, ParticipantCollectionChangedEventArgs args)
        {
            ((InstantMessageModality)args.Participant.Modalities[ModalityTypes.InstantMessage]).InstantMessageReceived -= InstantMessageModality_InstantMessageReceived;
        }

        void InstantMessageModality_InstantMessageReceived(object sender, MessageSentEventArgs args)
        {
            var imm = (InstantMessageModality)sender;
            ConversationContainer container = ActiveConversations[imm.Conversation];
            DateTime now = DateTime.Now;
            string allLog = string.Format(
                "{0}  {1}{2}{3}",
                imm.Participant.Contact.GetContactInformation(ContactInformationType.DisplayName).ToString().ToUpper(),
                now.ToString("HH:mm:ss  dd.MM.yyyy"),
                Environment.NewLine, 
                args.Text);

            using (var outfile = new StreamWriter(Path.Combine(MyDocsPath, ProgramFolder, AllInOneFileName), true))
            {
                outfile.WriteLine(allLog);
                outfile.Close();
            }

            foreach (Participant participant in container.Conversation.Participants)
            {
                if (participant.Contact == _myself.Contact)
                {
                    continue;
                }
                string directory = Path.Combine(
                                        MyDocsPath,
                                        ProgramFolder,
                                        participant.Contact.GetContactInformation(ContactInformationType.DisplayName).ToString());
                EnsureDirectoryExists(directory);

                string filename = string.Format("{0}\\{1}.txt", directory, now.ToString("yyyy-MM-dd"));
                string log = string.Format(
                    "{0}  {1}{2}{3}",
                    imm.Participant.Contact.GetContactInformation(ContactInformationType.DisplayName).ToString().ToUpper(),
                    now.ToString("HH:mm:ss"), 
                    Environment.NewLine, 
                    args.Text);

                using (var partfile = new StreamWriter(filename, true))
                {
                    partfile.WriteLine(log);
                    partfile.Close();
                }
            }

            ConsoleWriteLine(allLog);
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private LyncClient GetLyncClient()
        {
            LyncClient client = null;
            bool tryAgain = true;
            const int waitTime = 5;
            for (int attempts = 0; attempts < 20 && tryAgain; attempts++)
            {
                try
                {
                    tryAgain = false;
                    client = LyncClient.GetClient();
                }
                catch (LyncClientException)
                {
                    tryAgain = true;
                    if (attempts < 20)
                    {
                        ConsoleWriteLine(string.Format(Resources.ClientNotFoundTryingAgainInSeconds, waitTime));
                        System.Threading.Thread.Sleep(waitTime*1000);
                    }
                    else
                    {
                        ConsoleWriteLine(Resources.ClientNotFoundGivingUp);
                    }
                }
            }
            return client;
        }

        private void ConsoleWriteLine(string text = "")
        {
            if (_textAll.InvokeRequired)
            {
                SetText setText = ConsoleWriteLine;
                Invoke(setText, new object[] { text });
            }
            else
            {
                _textAll.AppendText(text + Environment.NewLine);
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        private void menuItemOpenAll_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(MyDocsPath, ProgramFolder, AllInOneFileName);
            if (!File.Exists(filePath))
            {
                MessageBox.Show(Resources.HistoryFileDoesNotExist);
                return;
            }
            Process.Start(filePath);
        }

        private bool _closingFromMenu;
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            _closingFromMenu = true;
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !_closingFromMenu)
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
        }

        private void menuItemOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(MyDocsPath, ProgramFolder));
        }
    }
}
