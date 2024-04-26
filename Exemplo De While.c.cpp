#include<stdio.h>
#include<stdlib.h>

int main(){
	int menu=1;
	while(menu!=0){
		system("cls");
		printf("escolha uma opcao:\n");
		printf("1- cadastro\n");
		printf("2- venda\n");
		printf("0- sair\n");
		printf("opcao:");
		scanf("%d",&menu);
	}
	return 0;
	
}
