grammar DensityExpressionGrammar;
options {language=CSharp_v4_0;}

compileUnit
    : expression + EOF
    ;

equation
   : expression relop expression
   ;

expression
   : term ((PLUS | MINUS) term)*
   ;

term
   : factor ((TIMES | DIV) factor)*
   ;

factor
   : PLUS factor
   | MINUS factor
   | atom
   ;

atom
   : number
   | variable
   | LPAREN expression RPAREN
   ;

number
   : NUMBER
   ;

variable
   : VARIABLE
   ;

relop
   : EQ
   | GT
   | LT
   ;


NUMBER
   : VALID_NUMBER_START VALID_NUMBER_CHAR*
   ;


VARIABLE
   : VALID_ID_START VALID_ID_CHAR*
   ;


fragment VALID_ID_START
   : ('a' .. 'z') | ('A' .. 'Z') | '_'
   ;


fragment VALID_ID_CHAR
   : VALID_ID_START | ('0' .. '9')
   ;


fragment VALID_NUMBER_START
   : ('1' .. '9')
   ;

fragment VALID_NUMBER_CHAR
   : VALID_NUMBER_START | '0'
   ;

fragment SIGN
   : ('+' | '-')
   ;


LPAREN
   : '('
   ;


RPAREN
   : ')'
   ;


PLUS
   : '+'
   ;


MINUS
   : '-'
   ;


TIMES
   : '*'
   ;


DIV
   : '/'
   ;


GT
   : '>'
   ;


LT
   : '<'
   ;


EQ
   : '='
   ;


WS
   : [ \r\n\t] + -> skip
   ;

