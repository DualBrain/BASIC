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
    PlusToken
    MinusToken
    StarToken
    SlashToken
    BangToken
    AmpersandAmpersandToken
    EqualsEqualsToken
    EqualsToken
    BangEqualsToken
    LessThanGreaterThanToken
    PipePipeToken
    OpenParenToken
    CloseParenToken
    IdentifierToken

    ' Keywords

    FalseKeyword
    TrueKeyword
    NotKeyword
    AndKeyword
    AndAlsoKeyword
    OrKeyword
    OrElseKeyword

    ' Expressions

    LiteralExpression
    UnaryExpression
    BinaryExpression
    ParenExpression

  End Enum

End Namespace