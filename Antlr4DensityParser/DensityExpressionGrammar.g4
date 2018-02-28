grammar DensityExpressionGrammar;
options {language=CSharp_v4_0;}

compileUnit:                   (density|probability) EOF ;

probability:                   density binaryBooleanOp density ;

density:                       term ((PLUS | MINUS) term)* ;
//                               | density CALL functionName LPAREN functionArguments? RPAREN ;

multiDensityList:              LBRACK density (SEP density)* RBRACK;

// functionName:                  strVariable ;

// functionArguments:             strVariable (SEP strVariable)* ;

term:                          factor ((TIMES | DIV) factor)* ;

factor:                          MINUS factor
                               | atom ;

atom:                            number
                               | variable
                               | multiDensityList
                               | LPAREN density RPAREN ;

number:                        NUMBER ;

variable:                      VARIABLE ;

strVariable:                   VARIABLE ;

binaryBooleanOp:                 EQ
                               | NEQ
                               | GT
                               | LT
                               | GE
                               | LE ;

NUMBER:                        '0' | VALID_NUMBER_START VALID_NUMBER_CHAR* ;
VARIABLE:                      VALID_ID_START VALID_ID_CHAR* ;

fragment VALID_ID_START:       ('a' .. 'z') | ('A' .. 'Z') | '_' ;
fragment VALID_ID_CHAR:        VALID_ID_START | ('0' .. '9') ;
fragment VALID_NUMBER_START:   ('1' .. '9') ;
fragment VALID_NUMBER_CHAR:    VALID_NUMBER_START | '0' ;

CALL:                          '.' ;
SEP:                           ',' ;
LPAREN:                        '(' ;
RPAREN:                        ')' ;
LBRACK:                        '[' ;
RBRACK:                        ']' ;
PLUS:                          '+' ;
MINUS:                         '-' ;
TIMES:                         '*' ;
DIV:                           '/' ;

EQ:                            '==' ;
NEQ:                           '<>' | '><' | '!=';
LT:                            '<' ;
LE:                            '<=' | '=<';
GT:                            '>' ;
GE:                            '=<' | '<=' ;

WS:                            [\r\n\t]+ -> skip ;
