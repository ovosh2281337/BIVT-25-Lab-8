using System;

namespace Lab8.Purple
{
    public class Task5
    {
        public class Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string Animal, string CharacterTrait, string Concept)
            {
                _animal = Animal;
                _characterTrait = CharacterTrait;
                _concept = Concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                int res = 0;

                for (int i = 0; i < responses.Length; i++)
                {
                    if (questionNumber == 1)
                    {
                        if (_animal != null && _animal != "" && responses[i].Animal == _animal)
                        {
                            res++;
                        }
                    }
                    if (questionNumber == 2)
                    {
                        if (_characterTrait != null && _characterTrait != "" && responses[i].CharacterTrait == _characterTrait)
                        {
                            res++;
                        }
                    }
                    if (questionNumber == 3)
                    {
                        if (_concept != null && _concept != "" && responses[i].Concept == _concept)
                        {
                            res++;
                        }
                    }
                }

                return res;
            }

            public void Print()
            {
                Console.WriteLine($"Животное: {Animal}, Черта: {CharacterTrait}, Понятие: {Concept}");
            }
        }

        public class Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses => _responses;

            public Research(string Name)
            {
                _name = Name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                string animal = "";
                string characterTrait = "";
                string concept = "";

                if (answers.Length > 0)
                {
                    animal = answers[0];
                }
                if (answers.Length > 1)
                {
                    characterTrait = answers[1];
                }
                if (answers.Length > 2)
                {
                    concept = answers[2];
                }

                Response newResponse = new Response(animal, characterTrait, concept);

                Response[] temp = new Response[_responses.Length + 1];
                for (int i = 0; i < _responses.Length; i++)
                {
                    temp[i] = _responses[i];
                }
                temp[_responses.Length] = newResponse;
                _responses = temp;
            }

            public string[] GetTopResponses(int question)
            {
                string[] uniqueAnswers = new string[0];
                int[] voteCounts = new int[0];

                for (int i = 0; i < _responses.Length; i++)
                {
                    string answer = "";

                    if (question == 1)
                    {
                        answer = _responses[i].Animal;
                    }
                    else if (question == 2)
                    {
                        answer = _responses[i].CharacterTrait;
                    }
                    else if (question == 3)
                    {
                        answer = _responses[i].Concept;
                    }

                    if (answer == null || answer == "")
                    {
                        continue;
                    }

                    bool found = false;

                    for (int j = 0; j < uniqueAnswers.Length; j++)
                    {
                        if (uniqueAnswers[j] == answer)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        string[] tempAnswers = new string[uniqueAnswers.Length + 1];
                        int[] tempCounts = new int[voteCounts.Length + 1];

                        for (int j = 0; j < uniqueAnswers.Length; j++)
                        {
                            tempAnswers[j] = uniqueAnswers[j];
                            tempCounts[j] = voteCounts[j];
                        }

                        tempAnswers[uniqueAnswers.Length] = answer;

                        string animal = "";
                        string characterTrait = "";
                        string concept = "";

                        if (question == 1)
                        {
                            animal = answer;
                        }
                        else if (question == 2)
                        {
                            characterTrait = answer;
                        }
                        else if (question == 3)
                        {
                            concept = answer;
                        }

                        Response countResponse = new Response(animal, characterTrait, concept);
                        tempCounts[voteCounts.Length] = countResponse.CountVotes(_responses, question);

                        uniqueAnswers = tempAnswers;
                        voteCounts = tempCounts;
                    }
                }

                for (int i = 0; i < uniqueAnswers.Length - 1; i++)
                {
                    for (int j = i + 1; j < uniqueAnswers.Length; j++)
                    {
                        if (voteCounts[i] < voteCounts[j])
                        {
                            (voteCounts[i], voteCounts[j]) = (voteCounts[j], voteCounts[i]);
                            (uniqueAnswers[i], uniqueAnswers[j]) = (uniqueAnswers[j], uniqueAnswers[i]);
                        }
                    }
                }

                int topCount = 5;
                if (uniqueAnswers.Length < 5)
                {
                    topCount = uniqueAnswers.Length;
                }

                string[] result = new string[topCount];
                for (int i = 0; i < topCount; i++)
                {
                    result[i] = uniqueAnswers[i];
                }

                return result;
            }

            public void Print()
            {
                Console.WriteLine($"Исследование: {Name}");
                Console.WriteLine("Ответы:");
                for (int i = 0; i < _responses.Length; i++)
                {
                    _responses[i].Print();
                }
            }
        }

        public class Report
        {
            private Research[] _researches;
            private static int _count;

            public Research[] Researches => _researches;

            static Report()
            {
                _count = 1;
            }

            public Report()
            {
                _researches = new Research[0];
            }

            public string Int22int(int num)
            {
                if (num.ToString().Length == 2)
                {
                    return num.ToString();
                }
                else
                {
                    return "0" + num.ToString();
                }
            }

            public void Add(Research someResearch)
            {
                Research[] temp = new Research[_researches.Length + 1];
                for (int i = 0; i < _researches.Length; i++)
                {
                    temp[i] = _researches[i];
                }
                temp[_researches.Length] = someResearch;
                _researches = temp;
            }

            public Research MakeResearch()
            {
                int X = _count;
                string MM = Int22int(DateTime.Now.Month);
                string YY = Int22int(DateTime.Now.Year % 100);

                Research newRes = new Research($"No_{X}_{MM}/{YY}");
                Add(newRes);
                _count++;

                return newRes;
            }

            public (string, double)[] GetGeneralReport(int question)
            {
                string[] uniqueAnswers = new string[0];
                int[] voteCounts = new int[0];
                int allVotesCount = 0;

                for (int i = 0; i < _researches.Length; i++)
                {
                    Response[] answers = _researches[i].Responses;

                    for (int j = 0; j < answers.Length; j++)
                    {
                        string answer = "";

                        if (question == 1)
                        {
                            answer = answers[j].Animal;
                        }
                        else if (question == 2)
                        {
                            answer = answers[j].CharacterTrait;
                        }
                        else if (question == 3)
                        {
                            answer = answers[j].Concept;
                        }

                        if (answer == null || answer == "")
                        {
                            continue;
                        }

                        allVotesCount++;

                        bool found = false;
                        for (int k = 0; k < uniqueAnswers.Length; k++)
                        {
                            if (uniqueAnswers[k] == answer)
                            {
                                voteCounts[k]++;
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            string[] tempAnswers = new string[uniqueAnswers.Length + 1];
                            int[] tempCounts = new int[voteCounts.Length + 1];

                            for (int k = 0; k < uniqueAnswers.Length; k++)
                            {
                                tempAnswers[k] = uniqueAnswers[k];
                                tempCounts[k] = voteCounts[k];
                            }

                            tempAnswers[uniqueAnswers.Length] = answer;
                            tempCounts[voteCounts.Length] = 1;

                            uniqueAnswers = tempAnswers;
                            voteCounts = tempCounts;
                        }
                    }
                }

                for (int i = 0; i < uniqueAnswers.Length - 1; i++)
                {
                    for (int j = i + 1; j < uniqueAnswers.Length; j++)
                    {
                        if (voteCounts[i] < voteCounts[j])
                        {
                            (voteCounts[i], voteCounts[j]) = (voteCounts[j], voteCounts[i]);
                            (uniqueAnswers[i], uniqueAnswers[j]) = (uniqueAnswers[j], uniqueAnswers[i]);
                        }
                    }
                }

                (string, double)[] result = new (string, double)[uniqueAnswers.Length];

                for (int i = 0; i < uniqueAnswers.Length; i++)
                {
                    double percent = 0;
                    if (allVotesCount > 0)
                    {
                        percent = (double)voteCounts[i] * 100.0 / allVotesCount;
                    }

                    result[i] = (uniqueAnswers[i], percent);
                }

                return result;
            }
        }
    }
}
