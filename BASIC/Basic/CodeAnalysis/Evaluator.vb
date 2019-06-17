﻿Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Binding

Namespace Global.Basic.CodeAnalysis

  Friend NotInheritable Class Evaluator

    Private m_lastValue As Object

    Sub New(root As BoundStatement, variables As Dictionary(Of VariableSymbol, Object))
      Me.Root = root
      Me.Variables = variables
    End Sub

    Public ReadOnly Property Root As BoundStatement
    Public ReadOnly Property Variables As Dictionary(Of VariableSymbol, Object)

    Public Function Evaluate() As Object
      Me.EvaluateStatement(Me.Root)
      Return Me.m_lastValue
    End Function

    Private Sub EvaluateStatement(node As BoundStatement)

      Select Case node.Kind
        Case BoundNodeKind.BlockStatement : Me.EvaluateBlockStatement(DirectCast(node, BoundBlockStatement))
        Case BoundNodeKind.VariableDeclaration : Me.EvaluateVariableDeclaration(DirectCast(node, BoundVariableDeclaration))
        Case BoundNodeKind.IfStatement : Me.EvaluateIfStatement(DirectCast(node, BoundIfStatement))
        Case BoundNodeKind.WhileStatement : Me.EvaluateWhileStatement(DirectCast(node, BoundWhileStatement))
        'Case BoundNodeKind.ForStatement : Me.EvaluateForStatement(DirectCast(node, BoundForStatement))
        Case BoundNodeKind.ExpressionStatement : Me.EvaluateExpressionStatement(DirectCast(node, BoundExpressionStatement))
        Case Else
          Throw New Exception($"Unexpected statement {node.Kind}")
      End Select

    End Sub

    Private Sub EvaluateBlockStatement(node As BoundBlockStatement)
      For Each statement In node.Statements
        Me.EvaluateStatement(statement)
      Next
    End Sub

    Private Sub EvaluateVariableDeclaration(node As BoundVariableDeclaration)
      Dim value = Me.EvaluateExpression(node.Initializer)
      Me.Variables(node.Variable) = value
      Me.m_lastValue = value
    End Sub

    Private Sub EvaluateIfStatement(node As BoundIfStatement)
      Dim condition = CBool(Me.EvaluateExpression(node.Condition))
      If condition Then
        Me.EvaluateStatement(node.ThenStatement)
      ElseIf node.ElseStatement IsNot Nothing Then
        Me.EvaluateStatement(node.ElseStatement)
      End If
    End Sub

    Private Sub EvaluateWhileStatement(node As BoundWhileStatement)
      While CBool(Me.EvaluateExpression(node.Condition))
        Me.EvaluateStatement(node.Body)
      End While
    End Sub

    'Private Sub EvaluateForStatement(node As BoundForStatement)
    '  Dim lowerBound = CInt(Me.EvaluateExpression(node.LowerBound))
    '  Dim upperBound = CInt(Me.EvaluateExpression(node.UpperBound))
    '  For i = lowerBound To upperBound
    '    Me.Variables(node.Variable) = i
    '    Me.EvaluateStatement(node.Body)
    '  Next
    'End Sub

    Private Sub EvaluateExpressionStatement(node As BoundExpressionStatement)
      Me.m_lastValue = Me.EvaluateExpression(node.Expression)
    End Sub

    Private Function EvaluateExpression(node As BoundExpression) As Object

      Select Case node.Kind
        Case BoundNodeKind.LiteralExpression : Return Me.EvaluateLiteralExpression(node)
        Case BoundNodeKind.VariableExpression : Return Me.EvaluateVariableExpression(node)
        Case BoundNodeKind.AssignmentExpression : Return Me.EvaluateAssignmentExpression(node)
        Case BoundNodeKind.UnaryExpression : Return Me.EvaluateUnaryExpression(node)
        Case BoundNodeKind.BinaryExpression : Return Me.EvaluateBinaryExpression(node)
        Case Else
          Throw New Exception($"Unexpected node {node.Kind}")
      End Select

    End Function

    Private Function EvaluateLiteralExpression(node As BoundExpression) As Object
      Return DirectCast(node, BoundLiteralExpression).Value
    End Function

    Private Function EvaluateVariableExpression(node As BoundExpression) As Object
      Dim v = DirectCast(node, BoundVariableExpression)
      Return Me.Variables(v.Variable)
    End Function

    Private Function EvaluateAssignmentExpression(node As BoundExpression) As Object
      Dim a = DirectCast(node, BoundAssignmentExpression)
      Dim value = Me.EvaluateExpression(a.Expression)
      Me.Variables(a.Variable) = value
      Return value
    End Function

    Private Function EvaluateUnaryExpression(node As BoundExpression) As Object
      Dim u = DirectCast(node, BoundUnaryExpression)
      Dim operand = Me.EvaluateExpression(u.Operand)
      Select Case u.Op.Kind
        Case BoundUnaryOperatorKind.Identity
          Return CInt(operand)
        Case BoundUnaryOperatorKind.Negation
          Return -CInt(operand)
        Case BoundUnaryOperatorKind.LogicalNegation
          Return Not CBool(operand)
        Case BoundUnaryOperatorKind.Onescomplement
          Return Not CInt(operand)
        Case Else
          Throw New Exception($"Unexpected unary operator {u.Op}")
      End Select
    End Function

    Private Function EvaluateBinaryExpression(node As BoundExpression) As Object
      Dim b = DirectCast(node, BoundBinaryExpression)
      Dim left = Me.EvaluateExpression(b.Left)
      Dim right = Me.EvaluateExpression(b.Right)
      Select Case b.Op.Kind
        Case BoundBinaryOperatorKind.Addition : Return CInt(left) + CInt(right)
        Case BoundBinaryOperatorKind.Subtraction : Return CInt(left) - CInt(right)
        Case BoundBinaryOperatorKind.Multiplication : Return CInt(left) * CInt(right)
        Case BoundBinaryOperatorKind.Division : Return CInt(left) \ CInt(right)
        Case BoundBinaryOperatorKind.BitwiseAnd
          If b.Type = GetType(Integer) Then
            Return CInt(left) And CInt(right)
          Else
            Return CBool(left) And CBool(right)
          End If
        Case BoundBinaryOperatorKind.BitwiseOr
          If b.Type = GetType(Integer) Then
            Return CInt(left) Or CInt(right)
          Else
            Return CBool(left) Or CBool(right)
          End If
        Case BoundBinaryOperatorKind.BitwiseXor
          If b.Type = GetType(Integer) Then
            Return CInt(left) Xor CInt(right)
          Else
            Return CBool(left) Xor CBool(right)
          End If
        Case BoundBinaryOperatorKind.LogicalAnd : Return CBool(left) And CBool(right)
        Case BoundBinaryOperatorKind.LogicalOr : Return CBool(left) Or CBool(right)
        Case BoundBinaryOperatorKind.Equals : Return Equals(left, right)
        Case BoundBinaryOperatorKind.NotEquals : Return Not Equals(left, right)
        Case BoundBinaryOperatorKind.Less : Return CInt(left) < CInt(right)
        Case BoundBinaryOperatorKind.Greater : Return CInt(left) > CInt(right)
        Case BoundBinaryOperatorKind.LessOrEquals : Return CInt(left) <= CInt(right)
        Case BoundBinaryOperatorKind.GreaterOrEquals : Return CInt(left) >= CInt(right)
        Case Else
          Throw New Exception($"Unexpected binary operator {b.Op}")
      End Select
    End Function

  End Class

End Namespace