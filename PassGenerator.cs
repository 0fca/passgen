using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassGen
{
	public class PassGenerator
	{
		private Boolean IS_UPPER_CASE = false; 
		private Boolean IS_LOWER_CASE = false;
		private Boolean IS_SYMBOLS_CHECK = false;
		private Boolean IS_NUMBER_CHECK = false;
		private int WORD_COUNT = 0;
		private Random random = new Random();
		private int LENG = 0;
		protected C5.ArrayList<char> WORD_DIC = new C5.ArrayList<char>();

		public void initialize(Boolean IS_UPPER_CASED,Boolean IS_LOWER_CASED,Boolean IS_SYMBOLS_CHECKED, Boolean IS_NUMBER_CHECKED,int word_count){
			this.IS_UPPER_CASE = IS_UPPER_CASED;
			this.IS_LOWER_CASE = IS_LOWER_CASED;
			this.IS_SYMBOLS_CHECK = IS_SYMBOLS_CHECKED;
			this.IS_NUMBER_CHECK = IS_NUMBER_CHECKED;
			this.WORD_COUNT = word_count;
		}

		public String getSequence(){
			return generateSequence();
		}

		protected String generateSequence(){
			String result = "";

			int next = 0;
			String lowerString = "abcdefghijklmnopqrstuvwxyząćęłśóżź";
			String upperString = "ABCDEFGHIJKLMNOPQRSTUVWXYZĄĆĘŁŚÓŻŹ";
			String numberString = "1234567890";
			String symbolString = "`~!#$%^&*()-_=+[{]};:,<.>?";
			String combinedStrings = "";
		  

			if(IS_LOWER_CASE){
				combinedStrings += lowerString;
			}

			if(IS_UPPER_CASE){ 
				combinedStrings += upperString;
			}

			if(IS_SYMBOLS_CHECK){
				combinedStrings += symbolString;
			}

			if (IS_NUMBER_CHECK) {
				combinedStrings += numberString;
			} 

			LENG = combinedStrings.Length-1;

			//Console.Write(WORD_COUNT);
			    char[] array = combinedStrings.ToCharArray();
				for(int i = 0; i<WORD_COUNT; i++){
				char next_char;
				if (combinedStrings.Length > 0) {
					next = random.Next(combinedStrings.Length);
					next_char = array[next];
	
						result += getRandom(combinedStrings);
					
				} else {
					next = random.Next(0,127);
					next_char = (char)next;
					result += next_char;
				}	
				}
			return result;
		}

		protected String getRandom(string combinedStrings)
		{
			string result = "";
			string rev = reverse(combinedStrings);
			string cutted = cut(rev);

			Console.Write (rev);
			int it = 0;

			while(true){
				if (it % 2 == 0) {
					int rand = random.Next (0, combinedStrings.Length);
					char c = cutted[rand];

					if (WORD_DIC.FindOrAdd (ref c)) {
						result = c.ToString();
						break;
					} 
				} else {
					int rand = random.Next (0, combinedStrings.Length);
					char c = combinedStrings[rand];

					if (WORD_DIC.FindOrAdd (ref c)) {
						result = c.ToString();
						break;
					} 
				}
				it++;
			}
			return  result;
		}

		private int findMinValue(){
			int? minVal = null; 
			int index = -1;
			char[] array = WORD_DIC.ToArray();

			for (int i = 0; i <array.Length; i++)
			{
				int thisNum = array[i];
				if (!minVal.HasValue || thisNum < minVal.Value)
				{
					minVal = thisNum;
					index = i;
				}
			}
			return (int)minVal;
		}

		private string reverse(string s)
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse( charArray );
			return new string( charArray );
		}

		private string cut(string to_cut){
			string cut = to_cut.Substring(0,to_cut.Length/2);
			string cut2 = to_cut.Substring (to_cut.Length / 2);
			return cut2+cut;		
		}
	
		private int convFloatToInt(float f){
			return Int32.Parse(f.ToString().Split('.')[0]);
		}
	}
}

