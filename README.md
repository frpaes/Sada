# README

            API para gerenciamento de itens.
    -> Métodos
        - Criar item
        - Consultar itens
        - Consultar item por Id
        - Atualizar item
        - Excluir item

    -> Status disponíveis
        Código / Descrição
        1 = Pendente
        2 = Em progresso
        3 = Concluído

    -> Exemplos
        - Busca todos os itens (podendo ser filtrados por status ou data)
        - Busca por ID (guid)
        - Criar item
        {
          "Titulo": "Cadastro item",
          "descricao": "Cadastro de item",
          "dataVencimento": "2026-05-10",
          "status": 1
        }
        - Atualiza item
        {
            "Titulo": "Atualiza item",
            "descricao": "Atualizar os itens",
            "dataVenciomnto": "2026-05-10",
            "status": 1
        }
        - Deleta registro pelo id(guid)
