# Biltiful - Documentação

### Estruturas de projeto
- Terá um projeto com três soluções para cada *Módulo* do sistema.
	- Os arquivos necessários para gravação e leitura de dados serão salvos na pasta raíz do projeto.

### Estrutura dos arquivos.


- Cada linha do arquivo terá que ter os dados separados pelo fim do tamanho da variavel e um cadastro por linha ex:
	
	- Cliente.dat
		- `cpf  nome  dnascimento sexo ucompra dcadastro situacao`		
	- Fornecedor.dat
		- `cnpj dabertura ucompra dcadastro situacao`
	- Materia.dat
		- `id nome ucompra dcadastro situacao`
	- Cosmetico.dat
		- `cbarras nome vvenda uvenda dcadastro situacao`
	- Risco.dat
		- `cpf`
	- Bloqueado.dat
		- `cpnj`
	- Venda.dat
		- `id dvenda cliente produto qtd vunitario titem produto qtd vunitario titem produto qtd vunitario titem vtotal`
	- Compra.dat
		- `id dcompra fornecedor mprima qtd vunitario mprima qtd vunitario mprima qtd vunitario vtotal`
	- Producao.dat
		- `id dproducao produto mprima qtdmp qtd`

## Versionamento
### Pull
-	O `pull` irá atualizar seu projeto local com as atualizações que tiveram no repositorio remoto. Uma boa prática, é, sempre que abrir o seu `Visual Studio` você dar um pull para que antes de você começar a escrever codigo tenha o projeto em sua ultima versão, isso irá previnir alguns futuros erros de `merge conflict`.
### Commits
- Quando suas alterações no código estiverem prontas, você terá que fazer um `commit`. Para fazer um `commit` você terá que descrever em poucas palavras as alterações feitas. Com o `commit` suas alterações estarão salvas localmente, mas não será o suficiente para *atualizar* o repositório remoto.
### Push
-	Depois de salvar suas alterações localmente é hora de mandar tudo para o repositório remoto, para que todos tenham acesso ao código atualizado. Porém, devemos ficar atento à alguns pontos: 
	-	Teve uma atualização enquanto você estava fazendo a sua. Com isso você deverá  fazer o `pull` antes de fazer `push`.
	-	Ao fazer o `pull` poderá acontecer de ter `merge conflicts`. Isso acontece por quê ocorreu alterações simultâneas no mesmo ponto de um arquivo. 
### Merge Conflicts
-	Quando ocorrer os `merge conflicts` você terá que análisar os arquivos que estão vindo e comparar com os que você está mandando e selecionar o qual irá manter. Após todos os conflitos resolvidos, será possivel fazer o `merge request`.
### Branches
- O projeto será trabalhado com uma Branch principal:
	-  `Master`.
	- Cada grupo terá uma branch de desenvolvimento.
		-  `Grupo1`, `Grupo2`,`Grupo3`.
			- Cada feature nova deverá ser criada um uma nova branch, e fazer o Pull Request (PR) na branch do seu grupo. O responsável por PR do grupo irá analisar o codigo e aceitar.
	- Em conjunto os responsáveis por PR irão analisar as três branches secundárias para o Pull Request para a `Master`.