using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Numerics;

namespace Lab8.Purple
{
    public class Task2
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks => _marks;
            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _marks = new int[5];
                _distance = 0;
                _target = 0;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                _distance = distance;
                _marks = marks;
                _target = target;
            }
            public int Result
            {
                get
                {
                    int total = 0;
                    int max = int.MinValue;
                    int min = int.MaxValue;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        total+=_marks[i];
                        if (_marks[i] < min)
                        {
                            min = _marks[i];
                        }
                        if (_marks[i] > max)
                        {
                            max = _marks[i];
                        }
                    }
                    total = total - min - max;
                    if (_distance != 120)
                    {
                        int distBALLS = 60 + (_distance - _target) * 2;
                        if (distBALLS < 0)
                        {
                            distBALLS = 0;
                        }
                        total = total + distBALLS;
                    }
                    else
                    {
                    total = total + 60;
                    }
                    return total;
                }
            }
            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}, Результат: {Result}");
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].Result < array[j].Result)
                        {
                            Participant temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;
            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;
            public SkiJumping(string Name, int standard)
            {
                _name = Name;
                _standard = standard;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                Participant[] res = new Participant[_participants.Length+1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    res[i] = _participants[i];
                }
                res[res.Length-1] = participant;
                _participants = res;
            }
            public void Add(Participant[] participants)
            {
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
            public void Jump(int distance, int[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Distance == 0)
                    {
                        _participants[i].Jump(distance, marks, _standard);
                        break;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Соревнование: {_name}, Норматив: {_standard}");
                for (int i = 0; i < _participants.Length; i++)
                {
                    _participants[i].Print();
                }
            }
        }
        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100)
            {
            }
        }
        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150)
            {
            }
        }
    }
}
