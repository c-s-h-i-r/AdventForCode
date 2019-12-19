namespace AdventForCode
{
    public class Day4 : BaseChallenge
    {
        //        --- Day 4: Secure Container ---
        //You arrive at the Venus fuel depot only to discover it's protected by a password.
        //The Elves had written the password on a sticky note, but someone threw it out.

        private int CriteriaMax { get; set; }
        private int CriteriaMin { get; set; }

        public Day4(string puzzleCriteria) : base("")
        {
            var inputs = puzzleCriteria.Split("-");
            if (!string.IsNullOrEmpty(inputs[0]) && !string.IsNullOrEmpty(inputs[0]))
            {
                CriteriaMin = int.Parse(inputs[0]);
                CriteriaMax = int.Parse(inputs[1]);
            }
        }

        public bool MeetsCriteria(string password)
        {
            var number = int.Parse(password);

            //It is a six-digit number.
            if (number < 100000 || number > 999999)
            {
                return false;
            }
            //The value is within the range given in your puzzle input.
            if (number > CriteriaMax || number < CriteriaMin)
            {
                return false;
            }

            var result = false;
            for (var i = 0; i < password.Length; i++)
            {
                //Two adjacent digits are the same (like 22 in 122345).
                result |= i > 0 && password[i] == password[i - 1] || i + 1 < password.Length && password[i] == password[i + 1];
                //Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
                if (i > 0 && password[i] < password[i - 1])
                {
                    return false;
                }
            }
            return result;
        }

        public int RunChallengePart1()
        {
            var result = 0;
            //How many different passwords within the range given in your puzzle input meet these criteria?
            for (var i = CriteriaMin; i <= CriteriaMax; i++)
            {
                if (MeetsCriteria(i.ToString()))
                {
                    result++;
                }
            }
            return result;
        }

        //        --- Part Two ---
        //An Elf just remembered one more important detail: the two adjacent matching digits are not part of a larger group of matching digits.
        //How many different passwords within the range given in your puzzle input meet all of the criteria?
        public int RunChallengePart2()
        {
            var result = 0;
            for (var i = CriteriaMin; i <= CriteriaMax; i++)
            {
                if (MeetsCriteriaUpdated(i.ToString()))
                {
                    result++;
                }
            }
            return result;
        }

        public bool MeetsCriteriaUpdated(string password)
        {
            var number = int.Parse(password);

            //It is a six-digit number.
            if (number < 100000 || number > 999999)
            {
                return false;
            }
            //The value is within the range given in your puzzle input.
            if (number > CriteriaMax || number < CriteriaMin)
            {
                return false;
            }

            char? prev = null;
            var seqCount = 1;
            var pairFound = false;
            for (var i = 0; i < password.Length; i++)
            {
                //Two adjacent digits are the same (like 22 in 122345).
                //the two adjacent matching digits are not part of a larger group of matching digits.
                if (prev.HasValue && password[i] == prev.Value)
                {
                    seqCount++;
                    if ((i == password.Length - 1 || password[i] != password[i + 1]) && seqCount == 1)
                    {
                        pairFound = true;
                    }
                }
                else
                {
                    seqCount = 0;
                }

                //Going from left to right, the digits never decrease; they only ever increase or stay the same(like 111123 or 135679).
                if (i > 0 && password[i] < password[i - 1])
                {
                    return false;
                }
                prev = password[i];
            }
            return pairFound;
        }
    }
}