using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mono.CSharp;

namespace PassGen
{
	public class PassGenerator
	{
		private Boolean IS_UPPER_CASE = false; 
		private Boolean IS_LOWER_CASE = false;
		private Boolean IS_SYMBOLS_CHECK = false;
		private bool IS_HEX_SELECTED = true;
		private bool IS_OCT_SELECTED = false;
		private int WORD_COUNT = 0;
		private int SYSTEM = 16;
		private int LENG = 0;  
		private List<string> DIC = new List<string>();
		private bool wasChanged = false;

		public void initialize(bool IS_UPPER_CASED,bool IS_LOWER_CASED,bool IS_SYMBOLS_CHECKED,bool IS_HEXAL_SELECTED,bool IS_OCTAL_SELECTED,int word_count){
			this.IS_UPPER_CASE = IS_UPPER_CASED;
			this.IS_LOWER_CASE = IS_LOWER_CASED;
			this.IS_SYMBOLS_CHECK = IS_SYMBOLS_CHECKED;
			this.WORD_COUNT = word_count;
			this.IS_HEX_SELECTED = IS_HEXAL_SELECTED;
			this.IS_OCT_SELECTED = IS_OCTAL_SELECTED;
		}

		public String getSequence(){
			return generateSequence();
		}

		protected String generateSequence(){
			string result = "";

			string lowerString = "abcdefghijklmnopqrstuvwxyząćęłśóżź";
			string upperString = "ABCDEFGHIJKLMNOPQRSTUVWXYZĄĆĘŁŚÓŻŹ";
			string symbolString = "`~!#$%^&*()-_=+[{]};:,<.>?";
			string combinedStrings = "";
		  

			if(IS_LOWER_CASE){
				combinedStrings += lowerString;
			}

			if(IS_UPPER_CASE){ 
				combinedStrings += upperString;
			}

			if(IS_SYMBOLS_CHECK){
				combinedStrings += symbolString;
			}


			if(IS_HEX_SELECTED){
				SYSTEM = 16;
			}

			if(IS_OCT_SELECTED){
				SYSTEM = 8;
			}

			LENG = combinedStrings.Length-1;

			    char[] array = combinedStrings.ToCharArray();

				for(int i = 0; i<=WORD_COUNT; i++){
					
					if (combinedStrings.Length > i||(array.Length-i) > i) {
					    Random random = new Random();
						char get = array[random.Next (i, array.Length)];
			
				  outter:{    
						while (true) {
							if (DIC.Contains (get.ToString ())) {
								get = array [random.Next (i, array.Length)];
								if (DIC.Contains(get.ToString ()))
									goto outter;
							} else {
								DIC.Add(get.ToString());
								result += translateSystem((int)get);
								break;
							}
						}
					}
					} else {
						Random random = new Random();
						char get = array[random.Next (0, array.Length)];
					    
						result += translateSystem((int)get);
					}
				}
			return result;
		}

		protected string translateSystem(int value)
		{
   			string output = System.Convert.ToString(value, SYSTEM);
			return shaffleCases(output);
		}

		private string reverse(string s)
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse( charArray );
			return new string(charArray);
		}
			
		private string shaffleCases(string inputSeq){


			if (!wasChanged) {
				wasChanged = true;
				return inputSeq.ToUpper();
			}else{
				wasChanged = false;
				return inputSeq.ToLower();
			}
		}
	}
}

