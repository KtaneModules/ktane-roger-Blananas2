using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System.Text.RegularExpressions;

public class rogerScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

    public KMSelectable[] pButtons;
    public KMSelectable Query;
    public TextMesh[] Rules; //0=Top, 1=Line, 2=Flavor text, 3=Instructions, 4=ID number, 5= Page number, 6=table
    public Material[] mats; //0=page 1, 1=normal, 2=morse
    public GameObject manual;
    public GameObject Button;
    public GameObject theEntireThing;

    private Coroutine buttonHold;
	private bool holding = false;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    int seed = 0;
    int xseed = 0;
    int page = 1;
    private List<string> good = new List<string> { };
    private List<string> zeros = new List<string> { "black", " 8.", "prime number, submit a ‘P’ in\nMorse Code.\n", "4 sides, press the button\nwhen the module timer forms a prime\nnumber.", "does not have an\nobtuse angle, press the button when the\nmodule timer forms an even number.", "2 different amounts,\nsubmit a ‘Y’", " 5\n", "[ones]", "2\nseconds, submit a ‘W’", "the\nnumber on the button." };
    private List<string> ones = new List<string> { "gray", " 11.", "square number, submit an ‘S’\nin Morse Code.\n", "an even number of sides,\npress the button when the module timer\nforms a prime number.", "has an obtuse\nangle, press the button when the module\ntimer forms an even number.", "3 different amounts,\nsubmit a ‘Q’", " 4\n", "[twos]", "3\nseconds, submit an ‘H’", "ten\nminus the number on the button." };
    public List<int> digits = new List<int> { };
    public List<int> answer = new List<int> { };
    public List<int> inputs = new List<int> { };
    public List<int> ruleSets = new List<int> { };
    int submitted = 0;
    float time = 0f;

    void Awake () {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable pButton in pButtons) {
            KMSelectable ppButton = pButton;
            pButton.OnInteract += delegate () { buttonPress(ppButton); return false; };
        }

        Query.OnInteract += delegate () { PressQuery(); return false; };
        Query.OnInteractEnded += delegate { QueryRelease(); };
    }

    // Use this for initialization
    void Start () {
        seed = UnityEngine.Random.Range(0, 10000);
        PageChange();
        xseed = seed;
        if (xseed > 8191) { good.Add(ones[0]); xseed -= 8192; } else { good.Add(zeros[0]); }
        if (xseed > 4095) { good.Add(ones[1]); xseed -= 4096; } else { good.Add(zeros[1]); }
        if (xseed > 2047) { good.Add(ones[2]); xseed -= 2048; } else { good.Add(zeros[2]); }
        if (xseed > 1023) { good.Add(ones[3]); xseed -= 1024; } else { good.Add(zeros[3]); }
        if (xseed > 511) { good.Add(ones[4]); xseed -= 512; } else { good.Add(zeros[4]); }
        if (xseed > 255) { good.Add(ones[5]); xseed -= 256; } else { good.Add(zeros[5]); }
        if (xseed > 127) { good.Add(ones[6]); xseed -= 128; } else { good.Add(zeros[6]); }
        if (xseed > 63) { good.Add(ones[7]); xseed -= 64; } else { good.Add(zeros[7]); }
        if (xseed > 31) { good.Add(ones[8]); xseed -= 32; } else { good.Add(zeros[8]); }
        if (xseed > 15) { good.Add(ones[9]); xseed -= 16; } else { good.Add(zeros[9]); }

        digits.Add((seed - seed % 1000)/1000);
        digits.Add(((seed - seed % 100)/100)%10);
        digits.Add(((seed - seed % 10)/10)%10);
        digits.Add(seed%10);

        switch (xseed) {
            case 0:  OrderRuleSets(3, 2, 1, 4); FormCorrectAnswer(1, 0, 3, 3, 0, 1, 2, 9); break;
            case 1:  OrderRuleSets(4, 3, 2, 1); FormCorrectAnswer(1, 1, 3, 4, 2, 1, 0, 5); break;
            case 2:  OrderRuleSets(1, 3, 4, 2); FormCorrectAnswer(2, 2, 3, 3, 1, 9, 0, 3); break;
            case 3:  OrderRuleSets(2, 1, 4, 3); FormCorrectAnswer(0, 2, 2, 5, 1, 1, 3, 4); break;
            case 4:  OrderRuleSets(3, 4, 1, 2); FormCorrectAnswer(0, 9, 2, 1, 1, 8, 3, 7); break;
            case 5:  OrderRuleSets(4, 2, 1, 3); FormCorrectAnswer(3, 5, 0, 1, 2, 5, 1, 0); break;
            case 6:  OrderRuleSets(1, 2, 3, 4); FormCorrectAnswer(3, 8, 2, 3, 1, 3, 0, 5); break;
            case 7:  OrderRuleSets(2, 3, 4, 1); FormCorrectAnswer(2, 2, 3, 2, 0, 9, 1, 2); break;
            case 8:  OrderRuleSets(3, 1, 2, 4); FormCorrectAnswer(3, 8, 1, 2, 2, 2, 0, 8); break;
            case 9:  OrderRuleSets(4, 1, 3, 2); FormCorrectAnswer(0, 9, 2, 1, 3, 0, 1, 2); break;
            case 10: OrderRuleSets(1, 4, 3, 2); FormCorrectAnswer(2, 9, 1, 8, 0, 3, 3, 3); break;
            case 11: OrderRuleSets(2, 3, 1, 4); FormCorrectAnswer(1, 4, 0, 8, 3, 0, 2, 7); break;
            case 12: OrderRuleSets(3, 1, 4, 2); FormCorrectAnswer(2, 6, 0, 0, 3, 8, 1, 7); break;
            case 13: OrderRuleSets(4, 1, 2, 3); FormCorrectAnswer(0, 1, 1, 4, 3, 7, 2, 3); break;
            case 14: OrderRuleSets(1, 3, 2, 4); FormCorrectAnswer(3, 6, 1, 5, 0, 9, 2, 3); break;
			case 15: OrderRuleSets(2, 1, 3, 4); FormCorrectAnswer(1, 0, 0, 7, 2, 3, 3, 8); break;
        }

        Debug.LogFormat("[Roger #{0}] The number on the first page of the manual is {1}{2}{3}{4}.", moduleId, digits[0], digits[1], digits[2], digits[3]);
        Debug.LogFormat("[Roger #{0}] The number at the bottom of the module should be {1}{2}{3}{4}.", moduleId, answer[0], answer[1], answer[2], answer[3]);
    }

	// Update is called once per frame
	void Update () {
        if (holding == true) {
            submitted = 0;
            inputs.Clear();
            holding = false;
            PageChange();
        }
	}

    void buttonPress(KMSelectable ppButton) {
        if (ppButton == pButtons[0] && page != 1) {
            page -= 1;
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.PageTurn, transform);
        } else if (ppButton == pButtons[1] && page != 7) {
            page += 1;
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.PageTurn, transform);
        }
        PageChange();
    }

    void PageChange() {
        Rules[6].text = "";
        Button.transform.localPosition = new Vector3( 0.009277344f, -0.0067f, -0.026f);
        Button.transform.localScale = new Vector3( 0.00001f, 0.00001f, 0.00001f);
        if (page != 1) {
            Rules[2].text = "";
            manual.GetComponent<MeshRenderer>().material = mats[1];
            Rules[4].text = "";
            if (page == 6) {
                Rules[0].text = String.Format("Final Step Numbers Submitted: {0}", submitted);
                Rules[1].transform.localPosition = new Vector3(-0.10705f, 0.1655386f, 0.0824f);
                Rules[1].transform.localScale = new Vector3(0.01604263f, 0.0004000004f, 1.1f);
                Rules[3].text = "Ask your expert what the numbers are at\nthe bottom of the module. Press the query\nbutton below when the last digit of the\ncountdown timer is those digits, in the\norder they are given.";
                Button.transform.localPosition = new Vector3( 0f, 0.0144f, -0.026f);
                Button.transform.localScale = new Vector3( 0.03f, 0.03f, 0.03f);
            } else if (page == 7) {
                manual.GetComponent<MeshRenderer>().material = mats[2];
                Rules[0].text = "Appendix That";
                Rules[1].transform.localPosition = new Vector3(-0.1116f, 0.1655386f, 0.0824f);
                Rules[1].transform.localScale = new Vector3(0.02211364f, 0.0004000004f, 1.1f);
                Rules[3].text = "The button has 4 attributes: A shape, a\ncolor, a character as it’s label, and\nflickering times.\n\nThe button will flicker WHITE and thus\nthe color of the button will never be\nwhite.\n\nThere are 4 indicator LEDs to indicate\nwhich stage the module is on. The ‘R’\nbutton in the bottom-right can be used\nto reset.\n\nThe module timer will start at 1 and\ngo up once a second and will wrap\nback to 0 after it reaches 59.\n\nHold the query button to reset inputs.\nNote that this does not work when submitting the last input.\n\nWhen submitting in Morse Code, holding over a tick in the\nmodule timer will create a dash. Not holding over a tick in the\ntimer will create a dot.";
            } else {
                if (ruleSets[page - 2] == 1) {
                    Rules[0].text = String.Format("Step {0}: Shape Pressing", page - 1);
                    Rules[1].transform.localPosition = new Vector3(-0.11954f, 0.1655386f, 0.0824f);
                    Rules[1].transform.localScale = new Vector3(0.03439362f, 0.0004000004f, 1.1f);
                    Rules[3].text = String.Format("If the button has {0}\n\nOtherwise, if the button {1}\n\nOtherwise, press the button when the\nmodule timer is odd and divisible by 3, and\nthe number on the module timer isn't 3\nitself.", good[3], good[4]);
                } else if (ruleSets[page - 2] == 2) {
                    manual.GetComponent<MeshRenderer>().material = mats[3];
                    Rules[0].text = String.Format("Step {0}: Character Holding", page - 1);
                    Rules[1].transform.localPosition = new Vector3(-0.12382f, 0.1655386f, 0.0824f);
                    Rules[1].transform.localScale = new Vector3(0.03964202f, 0.0004000004f, 1.1f);
                    Rules[3].text = String.Format("If the button has a number on it, hold the\nbutton for a number of seconds equal to {0}\n\nOtherwise, if the button’s label is an R, S,\nT, L, or N; hold the button for a number of\nseconds equal to the alphabetic position\nof the letter modulo{1}\n\nOtherwise, if the button’s\nlabel is a vowel, hold the\nbutton for a number of\nseconds according to the\ntable to the right.\n\nOtherwise, hold the button\nfor 7 seconds.", good[9], good[1]);
                    if (good[7] == "[ones]") {
                        Rules[6].text = "A         1\n\nE         2\n\nI         3\n\nO         4\n\nU         5";
                    } else {
                        Rules[6].text = "A         2\n\nE         4\n\nI         6\n\nO         8\n\nU        10";
                    }
                } else if (ruleSets[page - 2] == 3) {
                    Rules[0].text = String.Format("Step {0}: Color Tapping", page - 1);
                    Rules[1].transform.localPosition = new Vector3(-0.1195f, 0.1655386f, 0.0824f);
                    Rules[1].transform.localScale = new Vector3(0.03320019f, 0.0004000004f, 1.1f);
                    Rules[3].text = String.Format("If the button is red, press the button{0}times within 3 seconds.\n\nOtherwise, if the button is {1}, press the\nbutton 7 times within 3 seconds.\n\nOtherwise, press the button twice within\n3 seconds.", good[6], good[0]);
                } else if (ruleSets[page - 2] == 4) {
                    Rules[0].text = String.Format("Step {0}: Flickering Morse", page - 1);
                    Rules[1].transform.localPosition = new Vector3(-0.12225f, 0.1655386f, 0.0824f);
                    Rules[1].transform.localScale = new Vector3(0.03823728f, 0.0004000004f, 1.1f);
                    Rules[3].text = String.Format("If the button only flickers when the module\ntimer forms a {0}\nOtherwise, if the button flickers every {1} in Morse Code.\n\nOtherwise, if the delay between flickers\nchanges between {2} in Morse Code.\n\nOtherwise, submit an ‘X’ in Morse Code.", good[2], good[8], good[5]);
                }
            }
        } else {
            manual.GetComponent<MeshRenderer>().material = mats[0];
            Rules[0].text = "On the Subject of Roger";
            Rules[1].transform.localPosition = new Vector3(-0.121f, 0.1655386f, 0.0824f);
            Rules[1].transform.localScale = new Vector3(0.0357f, 0.0004000004f, 1.1f);
            Rules[2].text = "You hardly know 'er.";
            Rules[3].text = "\nThe module is not here, however\nit has been sent to your expert.\nHelp the expert get through the\nmodule so you can get rid of this manual to\nsee the status light. Please tell your\nexpert to input the number below into the\ntop-right display of the module to begin,\nthen follow the steps on the rest of the\npages in the order they are given.";
            if (seed > 999) {
                Rules[4].text = String.Format("{0}", seed);
            } else if (seed > 99) {
                Rules[4].text = String.Format("0{0}", seed);
            } else if (seed > 9) {
                Rules[4].text = String.Format("00{0}", seed);
            } else {
                Rules[4].text = String.Format("000{0}", seed);
            }
        }
        if (page == 7) {
            Rules[3].transform.localScale = new Vector3(0.0004000006f, 0.0004000006f, 1.1f);
        } else {
            Rules[3].transform.localScale = new Vector3(0.0006000006f, 0.0006000006f, 1.1f);
        }
        Rules[5].text = String.Format("Page {0} of 7", page);
    }

    void OrderRuleSets (int fir, int sec, int thi, int fou) {
        ruleSets.Add(fir);
        ruleSets.Add(sec);
        ruleSets.Add(thi);
        ruleSets.Add(fou);
    }

    void FormCorrectAnswer (int dig1, int add1, int dig2, int add2, int dig3, int add3, int dig4, int add4) {
        answer.Add((digits[dig1]+add1)%10);
        answer.Add((digits[dig2]+add2)%10);
        answer.Add((digits[dig3]+add3)%10);
        answer.Add((digits[dig4]+add4)%10);
    }

    void PressQuery () {
        Query.AddInteractionPunch();
        Audio.PlaySoundAtTransform("RogerThat", transform);
        submitted += 1;
        inputs.Add((int)Math.Floor(Bomb.GetTime()) % 10);
        if (submitted == 4) {
            page = 1;
            PageChange();
            if (inputs[0] == answer[0] && inputs[1] == answer[1] && inputs[2] == answer[2] && inputs[3] == answer[3]) {
                Debug.LogFormat("[Roger #{0}] You submitted {1}{2}{3}{4}, which is correct. Manual solved.", moduleId, inputs[0], inputs[1], inputs[2], inputs[3]);
                StartCoroutine(Shrink(true));
            } else {
                Debug.LogFormat("[Roger #{0}] You submitted {1}{2}{3}{4}, which is incorrect. Manual striked.", moduleId, inputs[0], inputs[1], inputs[2], inputs[3]);
                StartCoroutine(Shrink(false));
            }
        }
        PageChange();
        if (buttonHold != null)
		{
			holding = false;
			StopCoroutine(buttonHold);
			buttonHold = null;
		}

		buttonHold = StartCoroutine(HoldChecker());
    }

    void QueryRelease () {
        StopCoroutine(buttonHold);
    }

    private IEnumerator Shrink(bool right) {
        while(time < 1) {
            theEntireThing.transform.localScale = new Vector3((float)0.2f * (1 - time), 0.001f, (float)0.2f * (1 - time));
            yield return null;
            time += Time.deltaTime;
        }
        theEntireThing.transform.localScale = new Vector3(0.00001f, 0.001f, 0.00001f);
        time = 0;
        if (right == false) {
            GetComponent<KMBombModule>().HandleStrike();
            submitted = 0;
            inputs.Clear();
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Grow());
        } else {
            GetComponent<KMBombModule>().HandlePass();
        }
    }

    private IEnumerator Grow () {
        while(time < 1) {
            theEntireThing.transform.localScale = new Vector3((float)0.2f * time, 0.001f, (float)0.2f * time);
            yield return null;
            time += Time.deltaTime;
        }
        theEntireThing.transform.localScale = new Vector3(0.2f, 0.001f, 0.2f);
        time = 0;
    }

    IEnumerator HoldChecker()
	{
		yield return new WaitForSeconds(.4f);
        holding = true;
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} page <#> [Goes to manual page '#'] | !{0} query <#>(#)... [Presses the query button on manual page 6 when the last digit of the bomb's timer is '#' (optionally include multiple #'s)] | !{0} reset [Resets all inputs]";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*reset\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (page == 7)
            {
                pButtons[0].OnInteract();
                yield return new WaitForSeconds(0.1f);
            }
            else if (page < 6)
            {
                while (page < 6)
                {
                    pButtons[1].OnInteract();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            Query.OnInteract();
            yield return new WaitForSeconds(0.5f);
            Query.OnInteractEnded();
            yield break;
        }
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*query\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                int digit = 0;
                if (int.TryParse(parameters[1], out digit))
                {
                    if (digit < 0 || digit > 9999)
                    {
                        yield return "sendtochaterror The time(s) to press the query button '" + parameters[1].Join(", ") + "' is out of range 0-9999!";
                        yield break;
                    }
                    if (page == 7)
                    {
                        pButtons[0].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    else if (page < 6)
                    {
                        while (page < 6)
                        {
                            pButtons[1].OnInteract();
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    for (int i = 0; i < parameters[1].Length; i++)
                    {
                        int dig = int.Parse(parameters[1].ElementAt(i)+"");
                        while ((int)Bomb.GetTime() % 10 != dig)
                        {
                            yield return "trycancel Halted pressing the query button due to a request to cancel!";
                            yield return new WaitForSeconds(0.1f);
                        }
                        Query.OnInteract();
                        Query.OnInteractEnded();
                        yield return new WaitForSeconds(0.1f);
                    }
                    if (submitted == 4)
                    {
                        if (inputs[0] == answer[0] && inputs[1] == answer[1] && inputs[2] == answer[2] && inputs[3] == answer[3])
                        {
                            yield return "solve";
                        }
                        else
                        {
                            yield return "strike";
                        }
                    }
                }
                else
                {
                    yield return "sendtochaterror The time(s) to press the query button '" + parameters[1].Join(", ") + "' is invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the time(s) to press the query button!";
            }
            yield break;
        }
        if (Regex.IsMatch(parameters[0], @"^\s*page\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length > 2)
            {
                yield return "sendtochaterror Too many parameters!";
            }
            else if (parameters.Length == 2)
            {
                int digit = 0;
                if (int.TryParse(parameters[1], out digit))
                {
                    if (digit < 0 || digit > 7)
                    {
                        yield return "sendtochaterror The specified manual page to go to '" + parameters[1] + "' is out of range 0-7!";
                        yield break;
                    }
                    while (page > digit)
                    {
                        pButtons[0].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                    while (page < digit)
                    {
                        pButtons[1].OnInteract();
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                else
                {
                    yield return "sendtochaterror The specified manual page to go to '" + parameters[1] + "' is invalid!";
                }
            }
            else if (parameters.Length == 1)
            {
                yield return "sendtochaterror Please specify the manual page to go to!";
            }
            yield break;
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (time != 0) { yield return true; yield return new WaitForSeconds(0.1f); }
        if (submitted != 0)
        {
            for (int i = 0; i < submitted; i++)
            {
                if (inputs[i] != answer[i])
                {
                    yield return ProcessTwitchCommand("reset");
                }
            }
        }
        else
        {
            yield return ProcessTwitchCommand("page 6");
        }
        for (int i = 0; i < 4; i++)
        {
            while ((int)Bomb.GetTime() % 10 != answer[i])
            {
                yield return true;
                yield return new WaitForSeconds(0.1f);
            }
            Query.OnInteract();
            Query.OnInteractEnded();
            yield return new WaitForSeconds(0.1f);
        }
        while (time != 0) { yield return true; yield return new WaitForSeconds(0.1f); }
    }
}
