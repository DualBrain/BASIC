﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

  Public NotInheritable Class ReturnStatementSyntax
    Inherits StatementSyntax

    Public Sub New(returnKeyword As SyntaxToken, expression As ExpressionSyntax)
      Me.ReturnKeyword = returnKeyword
      Me.Expression = expression
    End Sub

    Public Overrides ReadOnly Property Kind() As SyntaxKind = SyntaxKind.ReturnStatement
    Public ReadOnly Property ReturnKeyword() As SyntaxToken
    Public ReadOnly Property Expression() As ExpressionSyntax

  End Class

End Namespace