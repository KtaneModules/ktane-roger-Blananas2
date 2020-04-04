//I apoligize in advance to you, the lovely person who decided to take the time to check the code for the module.
//This code is very sloppy (and somewhat rushed) and for that reason I must apologize.

var yourSeed = 0;
var presses = 0;
var seed = 0;
var answer = 0;
var bullshit = 0;
var binary = "";
var xseed = 0;
var stages = 0;
var shapes = ["hexagon", "square", "pentagon", "triangle"];
var labels = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
var colors = ["red", "blue", "black", "gray"];
var flicks = ["prime", "square", "two", "three", "24", "123"];
var bShape; var bLabel; var bColor; var bFlick;

var primes = "..##.#.#...#.#...#.#...#.....#.#.....#...#.#...#.....#.....#";
var squares = "##..#....#......#........#..........#............#..........";
var twosec = "#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.";
var threesec = "#..#..#..#..#..#..#..#..#..#..#..#..#..#..#..#..#..#..#..#..";
var twofour = "#..#....#..#....#..#....#..#....#..#....#..#....#..#....####";
var onetwothree = "#.#..#...#.#..#...#.#..#...#.#..#...#.#..#...#.#..#...#.#..#";

var timer = 0;
var before = 0;
var after = 0;

var uno = 0; var dos = 0; var tres = 0; var cuatro = 0;
var cinco = 0; var seis = 0; var siete = 0; var ocho = 0;
var order = "????";
var needs = "";
var inputs = "";
var currentSet = 'C';
var gucci = false;
var properClick = false;
var requiredMorse = 0;
var display = '0000';
var currentAction = '';
var firstPress = 0;
var damn = 0;
var MORSE = '';

function setup() {
  createCanvas(400, 400);
  frameRate(1);
  textSize(34);
  bShape = random(shapes);
  bLabel = random(labels);
  bColor = random(colors);
  bFlick = random(flicks);
  textAlign(CENTER, BASELINE);
}

function draw() {
  timer = frameCount % 60;
  trueTimer = frameCount;
  
  noStroke();
  
  if (gucci == true) {
    currentSet = order[stages];
  }
  
  if (damn > 0) {
    if (firstPress + 3 < trueTimer) {
      if (damn == 2) { inputs = inputs + '@'; display = '2t';}
      else if (damn == 4) { inputs = inputs + '$'; display = '4t';}
      else if (damn == 5) { inputs = inputs + '%'; display = '5t';}
      else if (damn == 7) { inputs = inputs + '&'; display = '7t';}
      else { inputs = inputs + '?'; display = damn + 't';}
      stages = stages + 1;
      damn = 0;
      
      if (stages == 4) {
        GoodGame();
      }
    }
  }
      
  
  background('#B9CEF0');
  fill(255, 255, 255);
  rect(295, 15, 90, 45);
  rect(15, 340, 90, 45);
  fill(0, 0, 0);
  rect(15, 15, 90, 45);
  fill(255, 0, 0);
  text(timer, 60, 49);
  fill(0, 0, 0);
  text(uno+""+dos+""+tres+""+cuatro, 340, 49);
  text(display, 60, 372);
  if (stages > 0) { fill(0, 255, 0); } else { fill(0, 0, 0); }
  circle(150, 363, 20);
  if (stages > 1) { fill(0, 255, 0); } else { fill(0, 0, 0); }
  circle(180, 363, 20);
  if (stages > 2) { fill(0, 255, 0); } else { fill(0, 0, 0); }
  circle(210, 363, 20);
  if (stages > 3) { fill(0, 255, 0); } else { fill(0, 0, 0); }
  circle(240, 363, 20);
  
  fill(255, 0, 0);
  circle(350, 360, 60);
  fill(0, 0, 0);
  text('R', 350, 370);
  
  //color
  if (bColor == "red") {
      fill(255, 0, 0);
  } else if (bColor == "blue") {
      fill(0, 0, 255);
  } else if (bColor == "gray") {
      fill(127, 127, 127);     
  } else if (bColor == "black") {
      fill(0, 0, 0);     
  }
  
  //flick
  if (bFlick == "prime") {
      if (primes[timer] == "#") { fill(255, 255, 255); }
  } else if (bFlick == "square") {
      if (squares[timer] == "#") { fill(255, 255, 255); }
  } else if (bFlick == "two") {
      if (twosec[timer] == "#") { fill(255, 255, 255); }
  } else if (bFlick == "three") {
      if (threesec[timer] == "#") { fill(255, 255, 255); }
  } else if (bFlick == "24") {
      if (twofour[timer] == "#") { fill(255, 255, 255); }
  } else if (bFlick == "123") {
      if (onetwothree[timer] == "#") { fill(255, 255, 255); }
  }
  
  //shape
  if (bShape == "hexagon") {
      quad(90, 200, 130, 100, 270, 300, 130, 300);
      quad(310, 200, 270, 100, 130, 100, 270, 300);
      quad(130, 100, 130, 300, 270, 300, 270, 100);
  } else if (bShape == "square") {
      quad(200, 100, 300, 200, 200, 300, 100, 200);
  } else if (bShape == "pentagon") {
      quad(200, 100, 300, 150, 145, 300, 100, 150);
      quad(200, 100, 300, 150, 255, 300, 100, 150);
      quad(200, 100, 300, 150, 255, 300, 145, 300);
  } else if (bShape == "triangle") {
      triangle(200, 100, 100, 300, 300, 300);
  }
  
  //label
  if (bShape != "triangle") {
    fill(255, 255, 255);
    textSize(75);
    text(bLabel, 200, 225);
    textSize(34);
  } else {
    fill(255, 255, 255);
    textSize(75);
    text(bLabel, 200, 245);
    textSize(34);
  }
}

function keyTyped() {
  presses = presses + 1;
  
  if (key === '0') { yourSeed = yourSeed * 10; }
  if (key === '1') { yourSeed = yourSeed * 10 + 1; }
  if (key === '2') { yourSeed = yourSeed * 10 + 2; }
  if (key === '3') { yourSeed = yourSeed * 10 + 3; }
  if (key === '4') { yourSeed = yourSeed * 10 + 4; }
  if (key === '5') { yourSeed = yourSeed * 10 + 5; }
  if (key === '6') { yourSeed = yourSeed * 10 + 6; }
  if (key === '7') { yourSeed = yourSeed * 10 + 7; }
  if (key === '8') { yourSeed = yourSeed * 10 + 8; }
  if (key === '9') { yourSeed = yourSeed * 10 + 9; }
  
  if (key === 'R') {
    seed = yourSeed;
    stages = 0;
    gucci = true;
    inputs = "";
    firstPress = 0;
    damn = 0;
    MORSE = '';
   }
  
  Calc();
}

function Calc() {
  yourSeed = yourSeed % 10000;
  
  uno = (yourSeed - yourSeed % 1000) / 1000;
  dos = ((yourSeed - yourSeed % 100) / 100) % 10;
  tres = ((yourSeed - yourSeed % 10) / 10) % 10;
  cuatro = yourSeed % 10;
  
  if (presses > 3) {
    seed = yourSeed;
    stages = 0;
    gucci = true;
    needs = "";
    inputs = "";
    firstPress = 0;
    damn = 0;
    MORSE = '';
    
    bShape = random(shapes);
    bLabel = random(labels);
    bColor = random(colors);
    bFlick = random(flicks);
    
    if (seed % 16 == 0) { order = "CBAD"; cinco = (dos + 0) % 10; seis = (cuatro + 3) % 10; siete = (uno + 1) % 10; ocho = (tres + 9) % 10;}
    if (seed % 16 == 1) { order = "DCBA"; cinco = (dos + 1) % 10; seis = (cuatro + 4) % 10; siete = (tres + 1) % 10; ocho = (uno + 5) % 10;}
    if (seed % 16 == 2) { order = "ACDB"; cinco = (tres + 2) % 10; seis = (cuatro + 3) % 10; siete = (dos + 9) % 10; ocho = (uno + 3) % 10;}
    if (seed % 16 == 3) { order = "BADC"; cinco = (uno + 2) % 10; seis = (tres + 5) % 10; siete = (dos + 1) % 10; ocho = (cuatro + 4) % 10;}
    if (seed % 16 == 4) { order = "CDAB"; cinco = (uno + 9) % 10; seis = (tres + 1) % 10; siete = (dos + 8) % 10; ocho = (cuatro + 7) % 10;}
    if (seed % 16 == 5) { order = "DBAC"; cinco = (cuatro + 5) % 10; seis = (uno + 1) % 10; siete = (tres + 5) % 10; ocho = (dos + 0) % 10;}
    if (seed % 16 == 6) { order = "ABCD"; cinco = (cuatro + 8) % 10; seis = (tres + 3) % 10; siete = (dos + 3) % 10; ocho = (uno + 5) % 10;}
    if (seed % 16 == 7) { order = "BCDA"; cinco = (tres + 2) % 10; seis = (cuatro + 2) % 10; siete = (uno + 9) % 10; ocho = (dos + 2) % 10;}
    if (seed % 16 == 8) { order = "CABD"; cinco = (cuatro + 8) % 10; seis = (dos + 2) % 10; siete = (tres + 2) % 10; ocho = (uno + 8) % 10;}
    if (seed % 16 == 9) { order = "DACB"; cinco = (uno + 9) % 10; seis = (tres + 1) % 10; siete = (cuatro + 0) % 10; ocho = (dos + 2) % 10;}
    if (seed % 16 == 10) { order = "ADCB"; cinco = (tres + 9) % 10; seis = (dos + 8) % 10; siete = (uno + 3) % 10; ocho = (cuatro + 3) % 10;}
    if (seed % 16 == 11) { order = "BCAD"; cinco = (dos + 4) % 10; seis = (uno + 8) % 10; siete = (cuatro + 0) % 10; ocho = (tres + 7) % 10;}
    if (seed % 16 == 12) { order = "CADB"; cinco = (tres + 6) % 10; seis = (uno + 0) % 10; siete = (cuatro + 8) % 10; ocho = (dos + 7) % 10;}
    if (seed % 16 == 13) { order = "DABC"; cinco = (uno + 1) % 10; seis = (dos + 4) % 10; siete = (cuatro + 7) % 10; ocho = (tres + 3) % 10;}
    if (seed % 16 == 14) { order = "ACBD"; cinco = (cuatro + 6) % 10; seis = (dos + 5) % 10; siete = (uno + 9) % 10; ocho = (tres + 3) % 10;}
    if (seed % 16 == 15) { order = "BACD"; cinco = (dos + 0) % 10; seis = (uno + 7) % 10; siete = (tres + 3) % 10; ocho = (cuatro + 8) % 10;}
  
    answer = cinco * 1000 + seis * 100 + siete * 10 + ocho;
    
    xseed = seed;
    binary = "";
    if (xseed > 8191) { binary = binary + "1"; xseed -= 8192; } else { binary = binary + "0"; }
    if (xseed > 4095) { binary = binary + "1"; xseed -= 4096; } else { binary = binary + "0"; }
    if (xseed > 2047) { binary = binary + "1"; xseed -= 2048; } else { binary = binary + "0"; }
    if (xseed > 1023) { binary = binary + "1"; xseed -= 1024; } else { binary = binary + "0"; }
    if (xseed > 511) { binary = binary + "1"; xseed -= 512; } else { binary = binary + "0"; }
    if (xseed > 255) { binary = binary + "1"; xseed -= 256; } else { binary = binary + "0"; }
    if (xseed > 127) { binary = binary + "1"; xseed -= 128; } else { binary = binary + "0"; }
    if (xseed > 63) { binary = binary + "1"; xseed -= 64; } else { binary = binary + "0"; }
    if (xseed > 31) { binary = binary + "1"; xseed -= 32; } else { binary = binary + "0"; }
    if (xseed > 15) { binary = binary + "1"; xseed -= 16; } else { binary = binary + "0"; }
  
    pickingFunctions(0);
    pickingFunctions(1);
    pickingFunctions(2);
    pickingFunctions(3);
  }
}

function mousePressed() {
  if (mouseX > 100 && 300 > mouseX && mouseY > 100 && 300 > mouseY) {
    before = trueTimer;
    properClick = true;
  } else if (mouseX > 300 && mouseY > 300) {
    seed = yourSeed;
    stages = 0;
    gucci = true;
    inputs = "";
    firstPress = 0;
    damn = 0;
    MORSE = '';
  } else if (mouseX > 300 && mouseY < 100) {
    presses = 4;
    yourSeed = prompt("Insert your four digit seed.");
    if (yourSeed.length != 4 || isNaN(yourSeed))
      return;
    seed = yourSeed;
    stages = 0;
    gucci = true;
    needs = "";
    inputs = "";
    firstPress = 0;
    damn = 0;
    MORSE = '';
    Calc();
  }
}

function mouseReleased() {
  if (properClick == true) {
    after = trueTimer;
    if (before != after) {
      currentAction = 'h';
    } else {
      currentAction = 't';
    }
    
    if (currentSet == 'A') {
        if (primes[timer] == '#') {
            inputs = inputs + 'p';
        } else if (timer % 2 == 0) {
            inputs = inputs + 'e';
        } else if (timer % 2 == 1 && timer % 3 == 0) {
            inputs = inputs + 't';
        } else {
            inputs = inputs + '?';
        }
        display = '#'+timer;
        stages = stages + 1;
    } else if (currentSet == 'B') {
        if (after - before == 1) { inputs = inputs + '1'; display = '1h';}
      else if (after - before == 2) { inputs = inputs + '2'; display = '2h';}
      else if (after - before == 3) { inputs = inputs + '3'; display = '3h';}
      else if (after - before == 4) { inputs = inputs + '4'; display = '4h';}
      else if (after - before == 5) { inputs = inputs + '5'; display = '5h';}
      else if (after - before == 6) { inputs = inputs + '6'; display = '6h';}
      else if (after - before == 7) { inputs = inputs + '7'; display = '7h';}
      else if (after - before == 8) { inputs = inputs + '8'; display = '8h';}
      else if (after - before == 9) { inputs = inputs + '9'; display = '9h';}
      else if (after - before == 10) { inputs = inputs + '~'; display = '10h';}
      else { inputs = inputs + '?'; }
      stages = stages + 1;
    } else if (currentSet == 'C') {
      if (firstPress == 0) {
        firstPress = trueTimer;
        damn = damn + 1;
      } else {
        if (trueTimer < firstPress + 3) {
          damn = damn + 1;
        }
      }
    } else if (currentSet == 'D') {
      if (currentAction == 'h') {
        MORSE = MORSE + '-';
      } else if (currentAction == 't') {
        MORSE = MORSE + '.';
      }
      if (MORSE.length == requiredMorse) {
        if (MORSE == ".--.") { inputs = inputs + 'P'; display = 'P';}
        else if (MORSE == "...") { inputs = inputs + 'S'; display = 'S'; }
        else if (MORSE == ".--") { inputs = inputs + 'W'; display = 'W'; }
        else if (MORSE == "....") { inputs = inputs + 'H'; display = 'H'; }
        else if (MORSE == "-.--") { inputs = inputs + 'Y'; display = 'Y'; }
        else if (MORSE == "--.-") { inputs = inputs + 'Q'; display = 'Q'; }
        else if (MORSE == "-..-") { inputs = inputs + 'X'; display = 'X'; }
        else if (MORSE == ".---") { inputs = inputs + 'B'; display = 'B'; }
        else if (MORSE == "-.-.") { inputs = inputs + 'C'; display = 'C'; }
        else if (MORSE == "-..") { inputs = inputs + 'D'; display = 'D'; }
        else if (MORSE == ".-..") { inputs = inputs + 'F'; display = 'F'; }
        else if (MORSE == "--.") { inputs = inputs + 'G'; display = 'G'; }
        else if (MORSE == ".---") { inputs = inputs + 'J'; display = 'J'; }
        else if (MORSE == "-.-") { inputs = inputs + 'K'; display = 'K'; }
        else if (MORSE == ".-..") { inputs = inputs + 'L'; display = 'L'; }
        else if (MORSE == "---") { inputs = inputs + 'O'; display = 'O'; }
        else if (MORSE == ".-.") { inputs = inputs + 'R'; display = 'R'; }
        else if (MORSE == "..-") { inputs = inputs + 'U'; display = 'U'; }
        else if (MORSE == "...-") { inputs = inputs + 'V'; display = 'V'; }
        else if (MORSE == "--..") { inputs = inputs + 'Z'; display = 'Z'; }
        else { inputs = inputs + '?'; }
        stages = stages + 1;
      }
    }
    
    if (stages == 4) {
      GoodGame();
    }
  }
  
  properClick = false;
}

function pickingFunctions(n) {
  if (order[n] == 'A') { setA(); }
  if (order[n] == 'B') { setB(); }
  if (order[n] == 'C') { setC(); }
  if (order[n] == 'D') { setD(); }
}

function setA() {
  if ((binary[3] == '0' && bShape == "square") || (binary[3] == '1' && (bShape == "square" || bShape == "hexagon"))) {
    needs = needs + 'p';
  } else if ((binary[4] == '0' && (bShape == "triangle" || bShape == "square")) || (binary[4] == '1' && (bShape == "pentagon" || bShape == "hexagon"))) {
    needs = needs + 'e';
  } else {
    needs = needs + 't';
  }
}

function setB() {
  if (bLabel == '1' || bLabel == '2' || bLabel == '3' || bLabel == '4' || bLabel == '5' || bLabel == '6' || bLabel == '7' || bLabel == '8' || bLabel == '9') {
    if (binary[9] == '0') {
      needs = needs + bLabel;
    } else {
      kappa(bLabel); //ten minus
    }
  } else if (bLabel == 'R' || bLabel == 'S' || bLabel == 'T' || bLabel == 'L' || bLabel == 'N') {
    if (binary[1] == '0') {
      kappa(bLabel + "%8");
    } else {
      kappa(bLabel + "%11");
    }
  } else if (bLabel == 'A' || bLabel == 'E' || bLabel == 'I' || bLabel == 'O' || bLabel == 'U') {
    if (binary[7] == '0') {
      kappa(bLabel + "<");  
    } else {
      kappa(bLabel + ">");
    }
  } else {
    needs = needs + '7';
  }
}

function kappa(x) {
  if (x == '1') { needs = needs + '9';}
  if (x == '2') { needs = needs + '8';}
  if (x == '3') { needs = needs + '7';}
  if (x == '4') { needs = needs + '6';}
  if (x == '5') { needs = needs + '5';}
  if (x == '6') { needs = needs + '4';}
  if (x == '7') { needs = needs + '3';}
  if (x == '8') { needs = needs + '2';}
  if (x == '9') { needs = needs + '1';}
  
  if (x == "R%8") { needs = needs + '2';}
  if (x == "S%8") { needs = needs + '3';}
  if (x == "T%8") { needs = needs + '4';}
  if (x == "L%8") { needs = needs + '4';}
  if (x == "N%8") { needs = needs + '6';}
  if (x == "R%11") { needs = needs + '7';}
  if (x == "S%11") { needs = needs + '8';}
  if (x == "T%11") { needs = needs + '9';}
  if (x == "L%11") { needs = needs + '1';}
  if (x == "N%11") { needs = needs + '3';}
  
  if (x == "A<") { needs = needs + '1';}
  if (x == "E<") { needs = needs + '2';}
  if (x == "I<") { needs = needs + '3';}
  if (x == "O<") { needs = needs + '4';}
  if (x == "U<") { needs = needs + '5';}
  if (x == "A>") { needs = needs + '2';}
  if (x == "E>") { needs = needs + '4';}
  if (x == "I>") { needs = needs + '6';}
  if (x == "O>") { needs = needs + '8';}
  if (x == "U>") { needs = needs + '~';} //tilda = 10
}

function setC() {
  if (bColor == "red") {
    if (binary[6] == '0') {
      needs = needs + '%';
    } else {
      needs = needs + '$';
    }
  } else if ((binary[0] == '0' && bColor == "black") || (binary[0] == '1' && bColor == "gray")) {
    needs = needs + '&';
  } else {
    needs = needs + '@';
  }
}

function setD() {
  if ((binary[2] == '0' && bFlick == "prime") || (binary[2] == '1' && bFlick == "square")) {
    if (binary[2] == '0') {
      needs = needs + 'P';
      requiredMorse = 4;
    } else {
      needs = needs + 'S';
      requiredMorse = 3;
    }
  } else if ((binary[8] == '0' && bFlick == "two") || (binary[8] == '1' && bFlick == "three")) {
    if (binary[8] == '0') {
      needs = needs + 'W';
      requiredMorse = 3;
    } else {
      needs = needs + 'H';
      requiredMorse = 4;
    }  
  } else if ((binary[5] == '0' && bFlick == "24") || (binary[5] == '1' && bFlick == "123")) {
    if (binary[5] == '0') {
      needs = needs + 'Y';
      requiredMorse = 4;
    } else {
      needs = needs + 'Q';
      requiredMorse = 4;
    } 
  } else {
    needs = needs + 'X';
    requiredMorse = 4;
  }
}

function GoodGame() {
  if (needs == inputs) {
    display = ((answer - answer % 1000) / 1000) + "" + (((answer - answer % 100) / 100) % 10) + "" + (((answer - answer % 10) / 10) % 10) + "" + (answer % 10);
  } else {
    bullshit = 0;
    if (inputs[0] == needs[0]) {
      bullshit = (answer - answer % 1000) / 1000;
    } else {
      bullshit = ((((answer - answer % 1000) / 1000) + ((seed - 1) % 9) + 1) % 10);
    }
    if (inputs[1] == needs[1]) {
      bullshit = bullshit * 10 + (((answer - answer % 100) / 100) % 10);
    } else {
      bullshit = bullshit * 10 + ((((answer - answer % 100) / 100) % 10 + ((seed - 1) % 9) + 1) % 10);
    }
    if (inputs[2] == needs[2]) {
      bullshit = bullshit * 10 + ((answer - answer % 10) / 10);
    } else {
      bullshit = bullshit * 10 + ((((answer - answer % 10) / 10) + ((seed - 1) % 9) + 1) % 10);
    }
    if (inputs[3] == needs[3]) {
      bullshit = bullshit * 10 + (answer % 10);
    } else {
      bullshit = bullshit * 10 + ((((answer % 10) + ((seed - 1) % 9) + 1) % 10));
    }
    display = ((bullshit - bullshit % 1000) / 1000) + "" + (((bullshit - bullshit % 100) / 100) % 10) + "" + (((bullshit - bullshit % 10) / 10) % 10) + "" + (bullshit % 10);
  }
}
