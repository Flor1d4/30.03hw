using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30._03hw
{
    class Program
    {
        public static void Registration(AbstractChatroom chatroom, Participant[] participants)
        {
            foreach (Participant participant in participants)
            {
                chatroom.Register(participant);
            }
        }

        public static void Chat(Participant participant, string to, string message)
        {
            participant.Send(to, message);
        }
        static void Main(string[] args)
        {
            AbstractChatroom chatroom = new Chatroom();

            Participant[] participants = new Participant[]
            {
                new Beatles("Pilot 1"),
                new Beatles("Pilot 2"),
                new Beatles("Pilot 3"),
                new Beatles("Pilot 4"),
                //new Beatles("All"),
                new NonBeatles("Dispetcher")
            };

            Registration(chatroom, participants);

            Chat(participants[2], "Dispetcher", "I'm in sector 0-1!");
            Chat(participants[4], "Pilot 1", "3th in sector 0-1.");
            Chat(participants[4], "Pilot 2", "3th in sector 0-1.");       
            Chat(participants[4], "Pilot 4", "3th in sector 0-1.");
           // Chat(participants[4], "All", "End ");
            Chat(participants[0], "Dispetcher", "I'm in sector 0-2!");
            Chat(participants[4], "Pilot 2", "1th in sector 0-2.");
            Chat(participants[4], "Pilot 3", "1th in sector 0-2.");
            Chat(participants[4], "Pilot 4", "1th in sector 0-2.");
           // Chat(participants[4], "All", "End of speech");
            Chat(participants[1], "Dispetcher", "I'm in sector 1-0!");
            Chat(participants[4], "Pilot 1", "2th in sector 1-0.");
            Chat(participants[4], "Pilot 3", "2th in sector 1-0.");
            Chat(participants[4], "Pilot 4", "2th in sector 1-0.");
           // Chat(participants[4], "All", "End of speech");
            Chat(participants[3], "Dispetcher", "I'm in sector 3-0!");
            Chat(participants[4], "Pilot 1", "4th in sector 3-0.");
            Chat(participants[4], "Pilot 2", "4th in sector 3-0.");
            Chat(participants[4], "Pilot 3", "4th in sector 3-0.");
            //Chat(participants[4], "All", "End of speech");


        }
    }
    public abstract class AbstractChatroom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(string from, string to, string message);
    }
    public abstract class Participant
    {
        public AbstractChatroom chatroom { get; set; }
        public string Name { get; set; }

        public Participant(string name)
        {
            Name = name;
        }
        public void Send(string to, string message)
        {
            chatroom.Send(Name, to, message);
        }
        public virtual void Receive(string from, string message)
        {
            Console.WriteLine($"{from} to {Name}: {message}");
        }
        public void SetChatroom(AbstractChatroom chatroom)
        {
            this.chatroom = chatroom;
        }
    }

    public class Chatroom : AbstractChatroom
    {
        Dictionary<string, Participant> participants = new Dictionary<string, Participant>();
        public override void Register(Participant participant)
        {
            if (!participants.ContainsKey(participant.Name))
            {
                participants[participant.Name] = participant;
            }
            participant.SetChatroom(this);
        }

        public override void Send(string from, string to, string message)
        {
            Participant participant = participants[to];
            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }

    class NonBeatles : Participant
    {

        public NonBeatles(string name) : base(name) { }
        public override void Receive(string from, string message)
        {
            Console.WriteLine("To Earth:");
            base.Receive(from, message);
        }
    }

    public class Beatles : Participant
    {
        public Beatles(string name) : base(name) { }
        public override void Receive(string from, string message)
        {
            Console.WriteLine("To Air: ");
            base.Receive(from, message);
        }
    }

   
}
