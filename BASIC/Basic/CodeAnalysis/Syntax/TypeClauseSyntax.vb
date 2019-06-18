﻿Option Explicit On
Option Strict On
Option Infer On

Namespace Global.Basic.CodeAnalysis.Syntax

  Public NotInheritable Class TypeClauseSyntax
    Inherits SyntaxNode

    Sub New(colonToken As SyntaxToken, identifier As SyntaxToken)
      Me.ColonToken = colonToken
      Me.Identifier = identifier
    End Sub

    Public Overrides ReadOnly Property Kind As SyntaxKind = SyntaxKind.TypeClause
    Public ReadOnly Property ColonToken As SyntaxToken
    Public ReadOnly Property Identifier As SyntaxToken

  End Class

End Namespace