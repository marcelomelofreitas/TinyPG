' Generated by TinyPG v1.1 available at www.codeproject.com

Imports System
Imports System.Collections.Generic


Namespace TinyPG
#Region "Parser"

    Partial Public Class Parser 
        Private m_scanner As Scanner
        Private m_tree As ParseTree

        Public Sub New(ByVal scanner As Scanner)
            m_scanner = scanner
        End Sub


    Public Function Parse(ByVal input As String) As ParseTree
            m_tree = New ParseTree()
            Return Parse(input, m_tree)
        End Function

        Public Function Parse(ByVal input As String, ByVal tree As ParseTree) As ParseTree
            m_scanner.Init(input)

            m_tree = tree
            ParseStart(m_tree)
            m_tree.Skipped = m_scanner.Skipped

            Return m_tree
        End Function

        Private Sub ParseStart(ByVal parent As ParseNode) ' NonTerminalSymbol: Start
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Start), "Start")
            parent.Nodes.Add(node)


             ' Concat Rule
            tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            While tok.Type = TokenType.DIRECTIVEOPEN
                ParseDirective(node) ' NonTerminal Rule: Directive
                tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            End While

             ' Concat Rule
            tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            While tok.Type = TokenType.SQUAREOPEN Or tok.Type = TokenType.IDENTIFIER
                ParseExtProduction(node) ' NonTerminal Rule: ExtProduction
                tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            End While

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: EOF
            If tok.Type <> TokenType.EOF Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EOF.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Start

        Private Sub ParseDirective(ByVal parent As ParseNode) ' NonTerminalSymbol: Directive
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Directive), "Directive")
            parent.Nodes.Add(node)


             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: DIRECTIVEOPEN
            If tok.Type <> TokenType.DIRECTIVEOPEN Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.DIRECTIVEOPEN.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: IDENTIFIER
            If tok.Type <> TokenType.IDENTIFIER Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.IDENTIFIER.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            While tok.Type = TokenType.IDENTIFIER
                ParseNameValue(node) ' NonTerminal Rule: NameValue
                tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            End While

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: DIRECTIVECLOSE
            If tok.Type <> TokenType.DIRECTIVECLOSE Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.DIRECTIVECLOSE.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Directive

        Private Sub ParseNameValue(ByVal parent As ParseNode) ' NonTerminalSymbol: NameValue
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.NameValue), "NameValue")
            parent.Nodes.Add(node)


             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: IDENTIFIER
            If tok.Type <> TokenType.IDENTIFIER Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.IDENTIFIER.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: ASSIGN
            If tok.Type <> TokenType.ASSIGN Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ASSIGN.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: CSTRING
            If tok.Type <> TokenType.CSTRING Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CSTRING.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: NameValue

        Private Sub ParseExtProduction(ByVal parent As ParseNode) ' NonTerminalSymbol: ExtProduction
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.ExtProduction), "ExtProduction")
            parent.Nodes.Add(node)


             ' Concat Rule
            tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            While tok.Type = TokenType.SQUAREOPEN
                ParseAttribute(node) ' NonTerminal Rule: Attribute
                tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            End While

             ' Concat Rule
            ParseProduction(node) ' NonTerminal Rule: Production

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: ExtProduction

        Private Sub ParseAttribute(ByVal parent As ParseNode) ' NonTerminalSymbol: Attribute
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Attribute), "Attribute")
            parent.Nodes.Add(node)


             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: SQUAREOPEN
            If tok.Type <> TokenType.SQUAREOPEN Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SQUAREOPEN.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: IDENTIFIER
            If tok.Type <> TokenType.IDENTIFIER Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.IDENTIFIER.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            tok = m_scanner.LookAhead() ' Option Rule
            If tok.Type = TokenType.BRACKETOPEN Then

                 ' Concat Rule
                tok = m_scanner.Scan() ' Terminal Rule: BRACKETOPEN
                If tok.Type <> TokenType.BRACKETOPEN Then
                    m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BRACKETOPEN.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                End If

                n = node.CreateNode(tok, tok.ToString() )
                node.Token.UpdateRange(tok)
                node.Nodes.Add(n)

                 ' Concat Rule
                tok = m_scanner.LookAhead() ' Option Rule
                If tok.Type = TokenType.CINTEGER Or tok.Type = TokenType.CDOUBLE Or tok.Type = TokenType.CSTRING Or tok.Type = TokenType.HEX Then
                    ParseParams(node) ' NonTerminal Rule: Params
                End If

                 ' Concat Rule
                tok = m_scanner.Scan() ' Terminal Rule: BRACKETCLOSE
                If tok.Type <> TokenType.BRACKETCLOSE Then
                    m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BRACKETCLOSE.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                End If

                n = node.CreateNode(tok, tok.ToString() )
                node.Token.UpdateRange(tok)
                node.Nodes.Add(n)
            End If

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: SQUARECLOSE
            If tok.Type <> TokenType.SQUARECLOSE Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SQUARECLOSE.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Attribute

        Private Sub ParseParams(ByVal parent As ParseNode) ' NonTerminalSymbol: Params
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Params), "Params")
            parent.Nodes.Add(node)


             ' Concat Rule
            ParseParam(node) ' NonTerminal Rule: Param

             ' Concat Rule
            tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            While tok.Type = TokenType.COMMA

                 ' Concat Rule
                tok = m_scanner.Scan() ' Terminal Rule: COMMA
                If tok.Type <> TokenType.COMMA Then
                    m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.COMMA.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                End If

                n = node.CreateNode(tok, tok.ToString() )
                node.Token.UpdateRange(tok)
                node.Nodes.Add(n)

                 ' Concat Rule
                ParseParam(node) ' NonTerminal Rule: Param
                tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            End While

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Params

        Private Sub ParseParam(ByVal parent As ParseNode) ' NonTerminalSymbol: Param
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Param), "Param")
            parent.Nodes.Add(node)

            tok = m_scanner.LookAhead()
            Select Case tok.Type
             ' Choice Rule
                Case TokenType.CINTEGER
                    tok = m_scanner.Scan() ' Terminal Rule: CINTEGER
                    If tok.Type <> TokenType.CINTEGER Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CINTEGER.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case TokenType.CDOUBLE
                    tok = m_scanner.Scan() ' Terminal Rule: CDOUBLE
                    If tok.Type <> TokenType.CDOUBLE Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CDOUBLE.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case TokenType.CSTRING
                    tok = m_scanner.Scan() ' Terminal Rule: CSTRING
                    If tok.Type <> TokenType.CSTRING Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CSTRING.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case TokenType.HEX
                    tok = m_scanner.Scan() ' Terminal Rule: HEX
                    If tok.Type <> TokenType.HEX Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.HEX.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case Else
                    m_tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", &H0002, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    Exit Select
            End Select ' Choice Rule

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Param

        Private Sub ParseProduction(ByVal parent As ParseNode) ' NonTerminalSymbol: Production
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Production), "Production")
            parent.Nodes.Add(node)


             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: IDENTIFIER
            If tok.Type <> TokenType.IDENTIFIER Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.IDENTIFIER.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            tok = m_scanner.Scan() ' Terminal Rule: ARROW
            If tok.Type <> TokenType.ARROW Then
                m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ARROW.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
            End If

            n = node.CreateNode(tok, tok.ToString() )
            node.Token.UpdateRange(tok)
            node.Nodes.Add(n)

             ' Concat Rule
            ParseRule(node) ' NonTerminal Rule: Rule

             ' Concat Rule
            tok = m_scanner.LookAhead()
            Select Case tok.Type
             ' Choice Rule
                Case TokenType.CODEBLOCK
                    tok = m_scanner.Scan() ' Terminal Rule: CODEBLOCK
                    If tok.Type <> TokenType.CODEBLOCK Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CODEBLOCK.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case TokenType.SEMICOLON
                    tok = m_scanner.Scan() ' Terminal Rule: SEMICOLON
                    If tok.Type <> TokenType.SEMICOLON Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SEMICOLON.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case Else
                    m_tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", &H0002, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    Exit Select
            End Select ' Choice Rule

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Production

        Private Sub ParseRule(ByVal parent As ParseNode) ' NonTerminalSymbol: Rule
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Rule), "Rule")
            parent.Nodes.Add(node)

            tok = m_scanner.LookAhead()
            Select Case tok.Type
             ' Choice Rule
                Case TokenType.CSTRING
                    tok = m_scanner.Scan() ' Terminal Rule: CSTRING
                    If tok.Type <> TokenType.CSTRING Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CSTRING.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case TokenType.IDENTIFIER
                Case TokenType.BRACKETOPEN
                    ParseSubrule(node) ' NonTerminal Rule: Subrule
                    Exit Select
                Case Else
                    m_tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", &H0002, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    Exit Select
            End Select ' Choice Rule

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Rule

        Private Sub ParseSubrule(ByVal parent As ParseNode) ' NonTerminalSymbol: Subrule
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Subrule), "Subrule")
            parent.Nodes.Add(node)


             ' Concat Rule
            ParseConcatRule(node) ' NonTerminal Rule: ConcatRule

             ' Concat Rule
            tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            While tok.Type = TokenType.PIPE

                 ' Concat Rule
                tok = m_scanner.Scan() ' Terminal Rule: PIPE
                If tok.Type <> TokenType.PIPE Then
                    m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PIPE.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                End If

                n = node.CreateNode(tok, tok.ToString() )
                node.Token.UpdateRange(tok)
                node.Nodes.Add(n)

                 ' Concat Rule
                ParseConcatRule(node) ' NonTerminal Rule: ConcatRule
                tok = m_scanner.LookAhead() ' ZeroOrMore Rule
            End While

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Subrule

        Private Sub ParseConcatRule(ByVal parent As ParseNode) ' NonTerminalSymbol: ConcatRule
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.ConcatRule), "ConcatRule")
            parent.Nodes.Add(node)

            Do ' OneOrMore Rule
                ParseSymbol(node) ' NonTerminal Rule: Symbol
                tok = m_scanner.LookAhead()
            Loop While tok.Type = TokenType.IDENTIFIER Or tok.Type = TokenType.BRACKETOPEN ' OneOrMore Rule

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: ConcatRule

        Private Sub ParseSymbol(ByVal parent As ParseNode) ' NonTerminalSymbol: Symbol
            Dim tok As Token
            Dim n As ParseNode
            Dim node As ParseNode = parent.CreateNode(m_scanner.GetToken(TokenType.Symbol), "Symbol")
            parent.Nodes.Add(node)


             ' Concat Rule
            tok = m_scanner.LookAhead()
            Select Case tok.Type
             ' Choice Rule
                Case TokenType.IDENTIFIER
                    tok = m_scanner.Scan() ' Terminal Rule: IDENTIFIER
                    If tok.Type <> TokenType.IDENTIFIER Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.IDENTIFIER.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case TokenType.BRACKETOPEN

                     ' Concat Rule
                    tok = m_scanner.Scan() ' Terminal Rule: BRACKETOPEN
                    If tok.Type <> TokenType.BRACKETOPEN Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BRACKETOPEN.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)

                     ' Concat Rule
                    ParseSubrule(node) ' NonTerminal Rule: Subrule

                     ' Concat Rule
                    tok = m_scanner.Scan() ' Terminal Rule: BRACKETCLOSE
                    If tok.Type <> TokenType.BRACKETCLOSE Then
                        m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BRACKETCLOSE.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    End If

                    n = node.CreateNode(tok, tok.ToString() )
                    node.Token.UpdateRange(tok)
                    node.Nodes.Add(n)
                    Exit Select
                Case Else
                    m_tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", &H0002, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                    Exit Select
            End Select ' Choice Rule

             ' Concat Rule
            tok = m_scanner.LookAhead() ' Option Rule
            If tok.Type = TokenType.UNARYOPER Then
                tok = m_scanner.Scan() ' Terminal Rule: UNARYOPER
                If tok.Type <> TokenType.UNARYOPER Then
                    m_tree.Errors.Add(New ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.UNARYOPER.ToString(), &H1001, 0, tok.StartPos, tok.StartPos, tok.EndPos - tok.StartPos))
                End If

                n = node.CreateNode(tok, tok.ToString() )
                node.Token.UpdateRange(tok)
                node.Nodes.Add(n)
            End If

            parent.Token.UpdateRange(node.Token)
        End Sub ' NonTerminalSymbol: Symbol


    End Class
#End Region
End Namespace

