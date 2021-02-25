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
		- `id dproducao produto mprima qtdmprima qtd`

### Versionamento
- O projeto será trabalhado com uma Branch principal:
	-  `Master`.
	- Cada grupo terá uma branch de desenvolvimento.
		-  `Grupo1`, `Grupo2`,`Grupo3`.
			- Cada feature nova deverá ser criada um uma nova branch, e fazer o Pull Request na branch do seu grupo. O responsável por PR do grupo irá analisar o codigo e aceitar.
	- Em conjunto os responsáveis por PR irão analisar as três branches secundárias para o Pull Request para a `Master`.
