﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

  Public Enum SyntaxKind

    ' Tokens
    BadToken
    EndOfFileToken
    WhitespaceToken
    NumberToken
    StringToken
    PlusToken
    MinusToken
    StarToken
    SlashToken
    BangToken
    EqualsToken
    TildeToken
    HatToken
    AmpersandToken
    AmpersandAmpersandToken
    EqualsEqualsToken
    BangEqualsToken
    LessThanToken
    LessThanEqualsToken
    LessThanGreaterThanToken
    GreaterThanEqualsToken
    GreaterThanToken
    PipeToken
    PipePipeToken
    OpenParenToken
    CloseParenToken
    OpenBraceToken
    CloseBraceToken
    CommaToken
    ColonToken
    IdentifierToken

    ' Keywords

    FalseKeyword
    TrueKeyword

    NotKeyword
    AndKeyword
    AndAlsoKeyword
    OrKeyword
    OrElseKeyword

    LetKeyword 'TODO: LET has a different behavior in BASIC.
    VarKeyword
    DimKeyword

    IfKeyword
    'ThenKeyword
    ElseKeyword
    'ElseIfKeyword
    'EndIfKeyword

    WhileKeyword
    DoKeyword

    ForKeyword
    ToKeyword

    ' Nodes

    CompilationUnit
    ElseClause
    TypeClause

    ' Statements

    BlockStatement
    VariableDeclaration
    IfStatement
    WhileStatement
    DoWhileStatement
    ForStatement
    ExpressionStatement

    ' Expressions

    LiteralExpression
    NameExpression
    UnaryExpression
    BinaryExpression
    ParenExpression
    AssignmentExpression
    CallExpression

  End Enum

End Namespace