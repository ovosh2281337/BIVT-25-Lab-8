using System.Security.Cryptography.X509Certificates;

namespace Lab8.Purple
{
    public class Task3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _marksCount;
            private bool _hasPerformed;
            public bool HasPerformed => _hasPerformed;
            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks => _marks;
            public int[] Places => _places;
            public int TopPlace
            {
                get
                {
                    int min = int.MaxValue;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        if (_places[i] < min)
                            min = _places[i];
                    }
                    return min;
                }
            }
            
            public double TotalMark
            {
                get
                {
                    double sum = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        sum+= _marks[i];
                    }
                    return sum;
                }
            }
            public int Score
            {
                get
                {
                    int res = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        res+=_places[i];
                    }
                    return res;
                }
            }
            public Participant(string Name, string Surname)
            {
                _name = Name;
                _surname = Surname;
                _marks = new double[7];
                _places = new int[7];
                _marksCount = 0;
            }
            public void Evaluate(double result)
            {
                if (_marksCount < 7)
                {
                    _marks[_marksCount] = result;
                    _marksCount++; 
                    if (_marksCount == 7)
                    {
                        _hasPerformed = true;
                    }
                }

            }
            public  static  void SetPlaces(Participant[]  participants)
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int i = 0; i < participants.Length; i++)
                    {
                        int place = 1;
                        for (int k = 0; k < participants.Length; k++)
                        {
                            if (participants[k].Marks[j] > participants[i].Marks[j])
                            {
                                place++;
                            }
                        }
                        participants[i].Places[j] = place;
                    }
                }
                for (int x = 0; x < participants.Length; x++)
                {
                    for (int y = x + 1; y < participants.Length; y++)
                    {
                        if (participants[x].Places[6] > participants[y].Places[6])
                        {
                            (participants[x], participants[y]) = (participants[y], participants[x]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}, Итоговая оценка: {TotalMark}, Сумма мест: {Score}, Лучшее место: {TopPlace}");
            }
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (array[i].Score > array[j].Score)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                        }
                        else if (array[i].Score == array[j].Score && array[i].TopPlace > array[j].TopPlace)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                        }
                        else if (array[i].Score == array[j].Score && array[i].TopPlace == array[j].TopPlace && array[i].TotalMark < array[j].TotalMark)
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                        }
                    }
                }
            }
        }
        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            public Participant[] Participants => (Participant[])_participants.Clone();
            public double[] Moods => (double[])_moods.Clone();
            public Skating(double[] Moods)
            {
                _moods = Moods  ;
                _participants = new Participant[0];   
                ModificateMood();
            }
            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].HasPerformed != true)
                    {
                        for (int j = 0; j < marks.Length; j++)
                        {
                            _participants[i].Evaluate(marks[j]*_moods[j]);
                        }
                        break;
                    }
                }
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
        }
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods)
            {
            }
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1) / 10.0;
                }
            }
        }
        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods)
            {
            }
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] *= 1+ (i + 1) / 100.0;
                }
            }
        }
    }

}
