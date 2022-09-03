#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <math.h>

#define M 5
#define L 5

//////////////////
// Ýkili Liste
/////////////////

//Düðüm yapýsý oluþturma
struct Node{
	char name[15];
	char surname[15];
	char department[25];
	int studentNum;
	struct Node * next;
	struct Node * prev;
	
	int takenXTimes;
};


struct Node* ALLhead;


struct Node* AKhead;


struct Node* LZhead;


struct Node* CreateNewNode(char name[],char surname[],char dep[],int num, int XTimes) {
	struct Node* newNode = (struct Node*)malloc(sizeof(struct Node));
	strcpy(newNode->name,name);
	strcpy(newNode->surname,surname);
	strcpy(newNode->department,dep);
	newNode->studentNum = num;
	newNode->prev = NULL;
	newNode->next = NULL;
	
	newNode->takenXTimes = XTimes;
	
	return newNode;
}


struct Node* InsertAtHead(struct Node* head, char name[],char surname[],char dep[],int num, int XTimes) {
	struct Node* newNode = CreateNewNode( name, surname,dep, num, XTimes);
	if(head == NULL) {
		head = newNode;
		return head;
	}
	head->prev = newNode;
	newNode->next = head; 
	head = newNode;
	
	return head;
}


struct Node* InsertAtTail(struct Node* head, char name[],char surname[],char dep[],int num, int XTimes) {
	struct Node* temp = head;
	struct Node* newNode = CreateNewNode( name, surname, dep, num, XTimes);
	if(head == NULL) {
		head = newNode;
		return head;
	}
	//En son Node'a git
	while(temp->next != NULL) temp = temp->next; 
	
	temp->next = newNode;
	newNode->prev = temp;
	
	return head;
}


void InsertIndexth(struct Node* head, char name[],char surname[],char dep[],int num,int index, int XTimes){
	int currIndex=2;
	struct Node* temp = head;
	struct Node* newNode = CreateNewNode( name, surname, dep, num, XTimes);
	//aðaç boþsa eklenen deðeri head'a atýyoruz
	if(head == NULL) {
		head = newNode;
		return;
	}
	//Istenen yeri bulana kadar çalýþacak
	for(currIndex;currIndex<index;currIndex++){
		temp=temp->next;
	}
	//eklenecek yerde deðer varsa üstüne yazmamak için sonraki deðeri saklýyoruz
	newNode->next=temp->next;
	
	temp->next=newNode;
	newNode->prev=temp;
	newNode->next->prev=newNode;
	
}


void DeleteNode(struct Node* head, int index){
	int currIndex=0;
	struct Node* temp = head;
	//Istenen yeri bulana kadar çalýþacak
	for(currIndex;currIndex<index;currIndex++){
		temp=temp->next;
	}
	//eðer silinmek istenen yer son deðer deðilse sonraki deðeri saklýyoruz
if(temp->next != NULL) 
    temp->next->prev = temp->prev; 
  
  //eðer silinmek istenen yer ilk deðer deðilse önceki deðeri saklýyoruz
  if(temp->prev != NULL) 
    temp->prev->next = temp->next;
    
    free(temp);
	
}


void SplitList(struct Node* head){
	
	struct Node * TempNode = head;

	while(TempNode != NULL){
		
		if(TempNode->surname[0] >= 'A' && TempNode->surname[0] <= 'K' || TempNode->surname[0] >= 'a' && TempNode->surname[0] <= 'k' ){	
			struct Node* newNode = CreateNewNode( TempNode->name, TempNode->surname,TempNode->department, TempNode->studentNum, TempNode->takenXTimes);
			
			if(AKhead == NULL) {
				AKhead = newNode;
				goto ContinueAK;
				}
				AKhead->prev = newNode;
				newNode->next = AKhead;
				AKhead = newNode;
				
				
			
		}ContinueAK:
	
			if(TempNode->surname[0] >= 'L' && TempNode->surname[0] <= 'Z' || TempNode->surname[0] >= 'l' && TempNode->surname[0] <= 'z' ){
			struct Node* newNode = CreateNewNode( TempNode->name, TempNode->surname,TempNode->department, TempNode->studentNum, TempNode->takenXTimes);
			if(LZhead == NULL) {
				LZhead = newNode;
				goto ContinueLZ;
			}
			LZhead->prev = newNode;
			newNode->next = LZhead;
			LZhead = newNode;
				
		}
		ContinueLZ:
		TempNode=TempNode->next;
	
		
	}
}


void PrintOriginal(struct Node* head) {
	struct Node* temp = head;
	printf("\nIleri yonde: \n");
	while(temp != NULL) {
//		printf("%d \n",temp->studentNum);
//		puts(temp->name);
//		puts(temp->surname);
//		puts(temp->department);
//		printf("%d", temp->takenXTimes);
//		printf("\n");
		
		printf(" %d %s %s %s %d \n\n", temp->studentNum, temp->name, temp->surname, temp->department, temp->takenXTimes);
		
		temp = temp->next;
	}
	printf("\n");
}


//void PrintFails(){
//	struct Node* temp1=head;
//	struct Node* temp2;
//	printf("\nDersi 1'den fazla kez alanlar\n");
//	if(temp1 == NULL)
//		printf("Liste Bos\n");
//	while(temp != NULL && temp->next != NULL){
//		temp2=temp1;
//		while(temp2->next != NULL){
//			if(temp1->studentNum==temp2->next->studentNum){
//				
//			}
//		}
//		
//	}
//}

void PrintOtherDepartments(struct Node* head){
	struct Node* temp = head;
	printf("\nBilgisayar Muhendisligi okumayip Bilgisayar Programlama dersi alanlar\n");
	if(temp == NULL)
		printf("Liste Bos\n");
	while(temp != NULL) {
		if(strcmp(temp->department,"BM") == 0 || strcmp(temp->department,"bm") == 0 || strcmp(temp->department ,"Bm") == 0 || strcmp(temp->department ,"bM")==0){
			temp = temp->next;
		}
		else{
			printf("%d \n",temp->studentNum);
			puts(temp->name);
			puts(temp->surname);
			puts(temp->department);
			printf("\n");
			temp = temp->next;
		}
	}
}


void PrintAK() {
	
	printf("\nIleri yonde(Soyadi A-K arasindakiler): \n");
	
	struct Node * Temp = AKhead;
	
	if(AKhead==NULL)
		printf("liste bos\n");
	while(Temp != NULL) {	
		printf("%d \n",Temp->studentNum);
		puts(Temp->name);
		puts(Temp->surname);
		puts(Temp->department);
		printf("\n");
		Temp = Temp->next;
	}
	printf("\n");
}


void PrintLZ() {
	
	printf("\nIleri yonde(Soyadi L-Z arasindakiler): \n");
	
	struct Node * Temp = LZhead;
	
	while(Temp != NULL) {
		printf("%d \n",Temp->studentNum);
		puts(Temp->name);
		puts(Temp->surname);
		puts(Temp->department);
		printf("\n");
		Temp = Temp->next;
	}
	printf("\n");
}


void PrintFromTail(struct Node* head) {
	struct Node* temp = head;
	if(temp == NULL) 
	return; // liste boþsa çýk
	
	// Listenin sonuna git
	while(temp->next != NULL) {
		temp = temp->next;
	}
	// prev pointer'ini kullanarak tersten yazdýr
	printf("Ters yonde: \n");
	while(temp != NULL) {
		
		printf("%d \n",temp->studentNum);
		puts(temp->name);
		puts(temp->surname);
		puts(temp->department);
		printf("\n");
		temp = temp->prev;
	}
	printf("\n");
}


/////////////////
// B+ Tree
////////////////

int intNull[1];
struct Student * studentNull;
struct BTreeNode * nodeNull;

struct Student{
	char name[15];
	char surname[15];
	char department[25];
	int studentNum;
	int takenXTimes;
};

struct BTreeNode {
	int n;     // Current number of keys
	int keys[M-1];  // An array of keys
	int leafKeys[L-1]; // An array of leaf keys
	bool isLeaf;
	struct BTreeNode *parentNode;
	// if internal node
    struct BTreeNode *childNodes[M]; // An array of child node pointers
    // if leaf
    struct Student * datas[L-1];  // An array of datas
    struct BTreeNode *right;
//    struct BTreeNode *left;
};


//struct BTreeNode * root; // Pointer to root node

struct BTreeNode * BTreeALL;
struct BTreeNode * BTreeAK;
struct BTreeNode * BTreeLZ;


// A
struct Node * ManyTimesBP_LL; // LL (linked list) 
struct BTreeNode * ManyTimesVYS_BTree;
struct Node * ManyTimesCommon_LL; // LL (linked list)

// B 
struct Node * OnlyBP_LL; // LL (linked list) 

// D
struct Node * bothButNotBM_LL; // LL (linked list) 

struct BTreeNode * CreateBTreeRoot( struct BTreeNode * root ){
	root = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
	root->n = 0;
	root->isLeaf = true;
	root->parentNode = NULL;
	root->right = NULL;
	return root;
}


struct BTreeNode * FindExpectedLeaf(struct BTreeNode * root ,int SKey){
	
	// Find the leaf node that the SKey supposed to be in
	
	printf("SKey: %d\n", SKey);
	int currFindIndex = 0;
	struct BTreeNode * currNode = root;
	
	bool isNext = false;
	
//	struct BTreeNode * result;
//	result = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
		
	printf("isLeaf: %s\n", currNode->isLeaf ? "true" : "false");
		
	// Find the leaf that the SKey supposed to be in
	while(currFindIndex < currNode->n && !currNode->isLeaf){
		
		isNext = false;
		
		printf("FindExpectedLeaf No: %d, curr Internal Key %d \n", SKey, currNode->keys[currFindIndex]);
		
		if( SKey < currNode->keys[currFindIndex] ){
			// go to left node of key
			printf("key %d SOL NODE GIT\n", currNode->keys[currFindIndex]);
			currNode = currNode->childNodes[currFindIndex];
			isNext = true;
		}else if(currNode->keys[currFindIndex] == SKey || currFindIndex == currNode->n - 1){
			// go to right node of key
			printf("key %d SAG NODE GIT\n", currNode->keys[currFindIndex]);
			currNode = currNode->childNodes[currFindIndex+1];
			isNext = true;
		}
		
		if(isNext){
			currFindIndex = 0;
		}else{
			currFindIndex++;
		}
		
		
	}
	
	return currNode;
	
}

struct Student * FetchStudent(struct BTreeNode * root, int SKey){
	
	// CAUTION: Use this function with isStudentRecorded(SKey)
	// if it returns true, use this function to fetch the related Student's data
	
	struct Student * result;
	result = (struct Student*) malloc(sizeof(struct Student));
	
	struct BTreeNode * currNode = FindExpectedLeaf(root, SKey);
	
	// Find the correct index of the key
	int currFindIndex = 0;

	while(currFindIndex<L-1){
		printf("Leaf curr %d (Find) %d\n", currFindIndex, currNode->leafKeys[currFindIndex]);
		if( currNode->leafKeys[currFindIndex] == SKey ){
			// Student found
			result = currNode->datas[currFindIndex];
			printf("Ogrenci kaydi bulundu \n\n");
			return result;
		}
		currFindIndex++;
	}
	
//	// Student not found
//	printf("\n findStudent bos dondu \n");
//	result->studentNum = 0;	
//	return result;

}

bool isStudentRecorded(struct BTreeNode * root, int SKey){
	
	struct BTreeNode * currNode = FindExpectedLeaf(root, SKey);
	
	// Check if the key exist in the leaf
	int currFindIndex = 0;
	printf("leafim ben\n");
	while(currFindIndex<currNode->n){
		printf("Leaf curr %d (Find) %d\n", currFindIndex, currNode->leafKeys[currFindIndex]);
		if( currNode->leafKeys[currFindIndex] == SKey ){
			// Student found
			printf("Ogrenci zaten kayitli \n\n");
			return true;
		}
		currFindIndex++;
	}
	
	// Student not found
	printf("\n Ogrenci kayitli degil \n");
	
	return false;

}

struct BTreeNode * findLowestLeaf(struct BTreeNode * root){
	
	struct BTreeNode * currNode = root;
	
	// check if root is leaf
	if(currNode->isLeaf){
		return currNode;
	}
	
	bool isCompleted = false;
	
	while(!isCompleted){
		currNode = currNode->childNodes[0];
		if(currNode->isLeaf){
			return currNode;
		}
	}
	
}

void printLeafs(struct BTreeNode * root){
	
	printf("----- PRINT LEAFS | start ------\n");
	
	struct BTreeNode * lowestLeaf = findLowestLeaf(root);
	
	struct BTreeNode * currLeaf = lowestLeaf;
	
	int leafCount = 1;
	
	while(currLeaf->right){
		
		printf("----- %d. Leaf ------\n", leafCount);
		for(int t=0; t<currLeaf->n; t++){
			printf("(Pri) currLeaf->keys[%d] : %d\n", t, currLeaf->leafKeys[t] );
		}
		
		leafCount++;
		currLeaf = currLeaf->right;
	}
	
	printf("----- %d. Leaf ------\n", leafCount);
	for(int t=0; t<currLeaf->n; t++){
		printf("(Pri) currLeaf->keys[%d] : %d\n", t, currLeaf->leafKeys[t] );
	}	
	
	printf("----- PRINT LEAFS | end ------\n");
	
}

void printLeafsV2(struct BTreeNode * root){
	
	printf("----- PRINT LEAFS | start ------\n");
	
	struct BTreeNode * lowestLeaf = findLowestLeaf(root);
	
	struct BTreeNode * currLeaf = lowestLeaf;
	
	int leafCount = 1;

	printf("----- %d. Leaf ------\n", leafCount);
	for(int t=0; t<currLeaf->n; t++){
		printf("(Pri) currLeaf->keys[%d] : %d\n", t, currLeaf->leafKeys[t] );
	}
	leafCount++;
	
	currLeaf = currLeaf->right;
	
	while(currLeaf){
		
		printf("----- %d. Leaf ------\n", leafCount);
		for(int t=0; t<currLeaf->n; t++){
			printf("(Pri) currLeaf->keys[%d] : %d\n", t, currLeaf->leafKeys[t] );
		}
		
		leafCount++;
		
		if(!currLeaf->right){
			currLeaf = currLeaf->right;
		}else{
			currLeaf = NULL;
		}
		
	}
	

	
	printf("----- PRINT LEAFS | end ------\n");
	
}

void printBplusTree(struct BTreeNode * root){
	
	printf("----- PRINT B+ Tree | start ------\n");
	
	struct BTreeNode * currNode = root;
	
	struct BTreeNode * leftStart = root;
	
	while(!leftStart->isLeaf){
		
		currNode = leftStart;
		
//		printf("currNode->right ? %s \n", currNode->right ? "true":"false");
		while( currNode->right ){
		
			for(int t=0; t<currNode->n; t++){
				printf("(%d) ", currNode->keys[t] );
			}
			printf(" (%s) | ", currNode->isLeaf ? "true":"false");
			currNode = currNode->right;
			
//			if(!currNode){
//				
//				break;
//			}
		}
		
		for(int t=0; t<currNode->n; t++){
			printf("(%d)", currNode->keys[t]);
		}
		printf("(%s)\n", currNode->isLeaf ? "true":"false");
		
		leftStart = leftStart->childNodes[0];
	}
	
	// Leafs
	
	struct BTreeNode * currLeaf = leftStart;
	
	while(currLeaf->right){
		
		for(int t=0; t<currLeaf->n; t++){
			printf("(%d)", currLeaf->leafKeys[t] );
		}
		printf(" (%s) | ", currLeaf->isLeaf ? "true":"false");
		currLeaf = currLeaf->right;
	}
	
	for(int t=0; t<currLeaf->n; t++){
		printf("(%d)", currLeaf->leafKeys[t] );
	}

	printf("(%s)\n", currLeaf->isLeaf ? "true":"false");
	
	printf("----- PRINT B+ Tree | end ------\n");
	
}


bool modifyInternalNode(struct BTreeNode * parInternalNode, int middleKey, struct BTreeNode * currInternalNode, struct BTreeNode * newInternalNode){
	
					// if the parent internal node has space for a new key
					int currIndex3 = 0;
					int maxFilledInd = 0;
					
					printf("Curr Internal Node N = %d", parInternalNode->n);
					
					currInternalNode->isLeaf = false;
					newInternalNode->isLeaf = false;

					
					// check if the Middle key is lower than a key and put it between
					for(currIndex3; currIndex3< parInternalNode->n; currIndex3++){
						
						printf("\n ( Modify Internal ) middleKey %d, curr key %d\n", middleKey, parInternalNode->keys[currIndex3]);
						
						if(middleKey < parInternalNode->keys[currIndex3]){
							// Push other values while adding new Middle key to correct index
							printf("Yeni Middle key \n");
							maxFilledInd = parInternalNode->n;
							

							
														
							
							for(maxFilledInd; currIndex3<=maxFilledInd; maxFilledInd--){
								parInternalNode->keys[maxFilledInd+1] = parInternalNode->keys[maxFilledInd];
								parInternalNode->childNodes[maxFilledInd+1] = parInternalNode->childNodes[maxFilledInd];
							}
							
//							parInternalNode->childNodes[parInternalNode->n+2] = newInternalNode;
							
							// DEBUG
							for(int t=0; t<newInternalNode->n; t++){
								printf("( Modify Internal ) newInternalNode->keys[%d] : %d\n", t, newInternalNode->keys[t] );
							}
							
							
							printf("( Modify Internal ) Current Index: %d\n", currIndex3);
							parInternalNode->keys[currIndex3] = middleKey;
							parInternalNode->childNodes[currIndex3] = currInternalNode;
							parInternalNode->childNodes[currIndex3+1] = newInternalNode;
							
//					printf("isLeaf??? parInternalNode->childNodes[%d]->keys[0] = %d  %s \n", currIndex3+1, parInternalNode->childNodes[currIndex3+1]->keys[0], parInternalNode->childNodes[currIndex3+1]->isLeaf ? "true":"false");
							
							parInternalNode->n = parInternalNode->n + 1;
							
							// DEBUG
							for(int t=0; t<parInternalNode->n; t++){
								printf("( Modify Internal ) parInternalNode->keys[%d] : %d\n", t, parInternalNode->keys[t] );
							}
							
							return true;
							
						}
						
					}
					
					// if it is the biggest key, add it to end
					
					// DEBUG
					for(int t=0; t<newInternalNode->n; t++){
						printf("( Modify Internal ) newInternalNode->keys[%d] : %d\n", t, newInternalNode->keys[t] );
					}
					
					
					int newLastIndex = parInternalNode->n;
					printf("( Modify Internal ) newLastIndex Index: %d\n", newLastIndex);
					parInternalNode->keys[newLastIndex] = middleKey;
					parInternalNode->childNodes[newLastIndex] = currInternalNode;
					parInternalNode->childNodes[newLastIndex+1] = newInternalNode;
					
					printf("isLeaf??? parInternalNode->childNodes[2]->keys[0] = %d  %s \n", parInternalNode->childNodes[2]->keys[0], parInternalNode->childNodes[2]->isLeaf ? "true":"false");

					
//					if(parInternalNode->){
//					}
	
					// Increase number of keys
					parInternalNode->n = parInternalNode->n + 1;
					
					
					
					// DEBUG
					for(int t=0; t<parInternalNode->n; t++){
						printf("( Modify Internal ) parInternalNode->keys[%d] : %d\n", t, parInternalNode->keys[t] );
					}
					
					printf("Internal Node duzenlendi. N: %d \n", parInternalNode->n);
					
					// be sure that the internal nodes point to their right nodes
					for(int y=0; y<parInternalNode->n-1; y++){
						parInternalNode->childNodes[y]->right = parInternalNode->childNodes[y+1];
					}	
					
					
					return true;
}


struct BTreeNode * splitInternalNode( struct BTreeNode * root, struct BTreeNode * currInternalNode, int leafMiddleKey, struct BTreeNode * currLeaf, struct BTreeNode * newLeaf, bool isRecurse ){

		// internal node is full
		
		printf("(Split Internal) leafMiddleKey: %d\n", leafMiddleKey);
		
		// Check if the currInternalNode is Root
		bool isCurrInternalRoot;
		struct BTreeNode * parNode = currInternalNode->parentNode;
		if(!parNode){
			isCurrInternalRoot = true;
		}else{
			isCurrInternalRoot = false;
		}
		
		
		if(isRecurse){
			currInternalNode->isLeaf = false;
			
			for(int y=0; y<currLeaf->n; y++){
				currLeaf->keys[y] = currLeaf->leafKeys[y];
				currLeaf->leafKeys[y] = *intNull;
			}
			currLeaf->isLeaf = false;
			
			for(int y=0; y<newLeaf->n; y++){
				newLeaf->keys[y] = newLeaf->leafKeys[y];
				newLeaf->leafKeys[y] = *intNull;
			}
			newLeaf->isLeaf = false;
		}
			
			struct BTreeNode * newRoot;
			newRoot = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
			
			if(isCurrInternalRoot){
				// Create parent node (new root)
				printf("\n currInternalNode Root! Yeni root oluþturuluyor. \n");
				newRoot->isLeaf = false;
				newRoot->n = 0;				
			}
			
			// Create a new internal node
			struct BTreeNode * newInternalNode;
			newInternalNode = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
			newInternalNode->isLeaf = false;
			newInternalNode->right = NULL;
			if(isCurrInternalRoot){
				newInternalNode->parentNode = newRoot;
			}
			
			
			// Find and place the new key to correct index
			// The internal node is full so 1 key will be temporarily saved to temporary variables
			
			bool completed2 = false;
			int currIndex2 = 0;
			bool isLessThanLast = false;
			
			int lastKeyTemp = 0;
			struct BTreeNode * tempChildLeft;
			struct BTreeNode * tempChildRight;
		
			int lastIndMinusOne = currInternalNode->n - 1;
			
			while(!completed2 && currIndex2<M-1){
				
//				printf("imdat sjsjs");
				
				if(leafMiddleKey < currInternalNode->keys[currIndex2]){
					
					// Push other values while adding new data to right index
					
					// if the new key is the biggest key save it as temp
					// if the new key is not biggest, save the last key of node as temp and place newValue to the correct index
					
					int maxFilledInd = currInternalNode->n-1;
					
					isLessThanLast = true;
					lastKeyTemp = currInternalNode->keys[M-2];
					tempChildLeft = currInternalNode->childNodes[M-1];
					tempChildRight = currInternalNode->childNodes[M];
											
					for(lastIndMinusOne; currIndex2<=lastIndMinusOne; lastIndMinusOne--){
						currInternalNode->keys[maxFilledInd+1] = currInternalNode->keys[maxFilledInd];
						currInternalNode->childNodes[maxFilledInd+1] = currInternalNode->childNodes[maxFilledInd];
					}
					
					currInternalNode->keys[currIndex2] = leafMiddleKey;
					currInternalNode->childNodes[currIndex2+1] = newLeaf;
	
					completed2 = true;
					
				}
				
				currIndex2++;
			}
			
			
			if(!isLessThanLast){
				lastKeyTemp = leafMiddleKey;
				tempChildLeft = currLeaf;
				tempChildRight = newLeaf;
			}
			
			// end of finding temp key and childNode
			
			// DEBUG
			for(int t=0; t<currInternalNode->n; t++){
				printf("(Split Internal) currInternalNode->keys[%d] : %d\n", t, currInternalNode->keys[t] );
			}



			// Split curr internal node
			
			int splitNodeMaxKeys1 = round(M/2); // first leaf
			int splitNodeMaxKeys2;
			
			if(M%2==0){
				splitNodeMaxKeys2 = splitNodeMaxKeys1 - 1; // second leaf
			}else{
				splitNodeMaxKeys2 = splitNodeMaxKeys1;
			}
			
			int splitNodeMaxInd1 = splitNodeMaxKeys1 - 1;
			int splitNodeMaxInd2 = splitNodeMaxKeys2 - 1;
			
				// Store Middle key
				int middleKey = currInternalNode->keys[splitNodeMaxInd1+1];
				
				
				printf("(Split Node) Internal MIDDLE KEY: %d\n", middleKey);
			
			// Fill new internal node
			int p=0;
			for(p; p<splitNodeMaxInd2; p++){
				printf("buraya bakarlar <o>\n");
				newInternalNode->keys[p] = currInternalNode->keys[p+splitNodeMaxKeys1+1];
				newInternalNode->childNodes[p] = currInternalNode->childNodes[p+splitNodeMaxKeys1];
				newInternalNode->childNodes[p]->parentNode = newInternalNode;
				
			}
			
			printf("(Split Node) P: %d", p);
			
			newInternalNode->keys[p] = lastKeyTemp;
			
			tempChildLeft->parentNode = newInternalNode;
			newInternalNode->childNodes[p] = tempChildLeft;
			
			tempChildRight->parentNode = newInternalNode;
			newInternalNode->childNodes[p+1] = tempChildRight;

			
			newInternalNode->n = splitNodeMaxKeys2;
			
			
//				for(int u=0; u<newInternalNode->n-1; u++){
//					newInternalNode->childNodes[u]->parentNode = newInternalNode;
//				}
			
			
			if(isCurrInternalRoot){
				newInternalNode->parentNode = newRoot;
			}else{
				newInternalNode->parentNode = currInternalNode->parentNode;
			}
			
			printf("yeni internal node'u doldurdum. N: %d \n", newInternalNode->n );

			
			
			// Delete old keys from splitted internal node
			for(int deleteInd = splitNodeMaxInd1+1; deleteInd<L-1; deleteInd++){
				currInternalNode->keys[deleteInd] = intNull[0];
				currInternalNode->childNodes[deleteInd] = nodeNull;
			}
			currInternalNode->n = splitNodeMaxKeys1;
//			currInternalNode->isLeaf = false;4			
			
			printf("eski yapragi düzelttim\n");
			
			// DEBUG
			for(int t=0; t<currInternalNode->n; t++){
				printf("(Split Internal) currInternalNode->keys[%d] : %d\n", t, currInternalNode->keys[t] );
			}
			for(int t=0; t<newInternalNode->n; t++){
				printf("(Split Internal) newInternalNode->keys[%d] : %d\n", t, newInternalNode->keys[t] );
			}
			
			
//			printLeafs();
			
			if(isCurrInternalRoot){
				currInternalNode->parentNode = newRoot;
			}
			
			// Point currInternalNode's right
//			if(currInternalNode->right){
//				newInternalNode->right = currInternalNode->right;
//			}
			
			currInternalNode->right = newInternalNode;
			
			
			// Prepare newRoot
			if(isCurrInternalRoot){
				newRoot->n = 1;
				newRoot->keys[0] = middleKey;
				newRoot->right = NULL;
				newRoot->childNodes[0] = currInternalNode;
				newRoot->childNodes[1] = newInternalNode;
				
			}

			
			if(isCurrInternalRoot){
				// At the end, root points to newRoot now
				root = newRoot;
				return root;
			}
			
			
			// if currInternal not root, check if upperNode has space			
			if(!isCurrInternalRoot){
				printf("currInternalNode Root degil!\n");
				struct BTreeNode * upperNode = currInternalNode->parentNode;
				
//				for(int t=0; t<currInternalNode->n; t++){
//					printf("(Split Internal) currInternalNode->keys[%d] : %d\n", t, currInternalNode->keys[t] );
//				}
				printf(" upperNode %d key iceriyor\n", upperNode->n);
				if( upperNode->n < M-1){
					// upperNode is not full
					printf("upperNode is not full\n");
					modifyInternalNode(upperNode, middleKey, currInternalNode, newInternalNode);
				}else{
					// upperNode is FULL
					printf("upperNode is FULL");
					root = splitInternalNode(root, upperNode, middleKey, currInternalNode, newInternalNode, true);
				}
			}
			
//			// DEBUG
//			printf("Root keys:\n");
//			for(int t=0; t<root->n; t++){
//				printf("(Split Internal) root->keys[%d] : %d\n", t, root->keys[t] );
//			}			
	
			return root;
}



struct BTreeNode * InsertStudent(struct BTreeNode * root, struct Student * newStudent){
	
	printf("\n ---------- \n Eklenmek istenen ogrenci: %d \n", newStudent->studentNum);
	
	struct Student * searchStudent;
	struct Student * newRecord;
		
	// Check if it is already recorded
	
	if( isStudentRecorded( root, newStudent->studentNum ) ){
// 		searchStudent = FetchStudent(root, newStudent->studentNum);
		printf("Ogrenci zaten kayitli.");
		return root;
	}
	
	// Insert
	
	struct BTreeNode * currLeaf = FindExpectedLeaf( root, newStudent->studentNum );
	
	int currIndex = 0;

	bool completed = false;

	int tempOldValue = 0;
	int maxFilledInd = 0;
	
	if(currLeaf->n < L-1 ){
		// Leaf has space for new data
		
		printf("Leaf n: %d\n", currLeaf->n);
	
		currIndex = 0;
		
		// if the leaf is empty (only possible if it is root)
		if(currLeaf->n==0){
			currLeaf->leafKeys[0] = newStudent->studentNum;
			currLeaf->datas[0] = newStudent;
			currLeaf->n = currLeaf->n + 1;
			return root;
		}
		
		// check if it is lower than a key and put it between
		for(currIndex; currIndex< currLeaf->n; currIndex++){
			
			printf("\n (Insert) ogr no %d, curr key %d\n", newStudent->studentNum, currLeaf->leafKeys[currIndex]);
			
			if(newStudent->studentNum < currLeaf->leafKeys[currIndex]){
				// Push other values while adding new data to right index
				printf("yeni key \n");
				maxFilledInd = currLeaf->n - 1;
				
				for(maxFilledInd; currIndex<=maxFilledInd; maxFilledInd--){
					currLeaf->leafKeys[maxFilledInd+1] = currLeaf->leafKeys[maxFilledInd];
					currLeaf->datas[maxFilledInd+1] = currLeaf->datas[maxFilledInd];
				}
				
				
				currLeaf->leafKeys[currIndex] = newStudent->studentNum;
				currLeaf->datas[currIndex] = newStudent;
				
				
				completed = true;
				
				// Increase number of keys
				currLeaf->n = currLeaf->n + 1;
				
				printf("ekledim\n");
				

				newRecord = currLeaf->datas[currIndex];
				
				printf("%s", newRecord->name);
				
				
				return root;
			}
			
		}
		
		// if it is the biggest key, add it to end
		currLeaf->leafKeys[currIndex] = newStudent->studentNum;
		currLeaf->datas[currIndex] = newStudent;
		
		currLeaf->n = currLeaf->n + 1;
		
		return root;
		
	}else{
		
		// leaf is full
		// currLeaf is LEAF!
		
		printf("\n Yaprak dolu! \n");
		
		// Check if the parent node is Root
		bool isRootLeaf;
		struct BTreeNode * parNode = currLeaf->parentNode;
		if(!parNode){
			isRootLeaf = true;
		}else{
			isRootLeaf = false;
		}
			
			
			struct BTreeNode * newRoot;
			newRoot = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
			
			if(isRootLeaf){
				// Create parent node (new root)
				printf("\n Root yaprak! Yeni root oluþturuluyor. \n");
				newRoot->isLeaf = false;
				newRoot->n = 0;				
			}
			
			// Create a new leaf node
			struct BTreeNode * newLeaf;
			newLeaf = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
			newLeaf->isLeaf = true;
			newLeaf->right = NULL;
			if(isRootLeaf){
				newLeaf->parentNode = newRoot;
			}
			// if currLeaf is not the last leaf, transfer currLeaf's right to newLeafs right
			if(currLeaf->right){
				newLeaf->right = currLeaf->right;
			}
			
			
			// Find and place the new key&data to correct index
			// The leaf is full so 1 key will be temporarily saved to temporary variables
			
			bool completed2 = false;
			int currIndex2 = 0;
			bool isLessThanLast = false;
			
			int lastKeyTemp = 0;
			struct Student * lastDataTemp;
		
			int lastIndMinusOne = currLeaf->n - 1;
			
			while(!completed2 && currIndex2<L-1){
				
//				printf("imdat sjsjs");
				
				if(newStudent->studentNum < currLeaf->leafKeys[currIndex2]){
					
					// Push other values while adding new data to right index
					
					// if the new key is the biggest key save it as temp
					// if the new key is not biggest, save the last key of node as temp and place newValue to the correct index
					
					isLessThanLast = true;
					lastKeyTemp = currLeaf->leafKeys[L-2];
					lastDataTemp = currLeaf->datas[L-2];
											
					for(lastIndMinusOne; currIndex2<=lastIndMinusOne; lastIndMinusOne--){
						currLeaf->leafKeys[lastIndMinusOne+1] = currLeaf->leafKeys[lastIndMinusOne];
						currLeaf->datas[lastIndMinusOne+1] = currLeaf->datas[lastIndMinusOne];
						
					}
					
					currLeaf->leafKeys[currIndex2] = newStudent->studentNum;
					currLeaf->datas[currIndex2] = newStudent;
	
					completed2 = true;
					
				}
				
				currIndex2++;
			}
			
			
			if(!isLessThanLast){
				lastKeyTemp = newStudent->studentNum;
				lastDataTemp = newStudent;
			}
			
			// end of finding temp key&data pair


			// Split the leaf
			
			int splitLeafMaxKeys1 = round(L/2); // first leaf
			int splitLeafMaxKeys2;
			
			if(L%2==0){
				splitLeafMaxKeys2 = splitLeafMaxKeys1; // second leaf
			}else{
				splitLeafMaxKeys2 = splitLeafMaxKeys1 + 1;
			}
			
			int splitLeafMaxInd1 = splitLeafMaxKeys1 - 1;
			int splitLeafMaxInd2 = splitLeafMaxKeys2 - 1;
			
				// Store Middle key
				int middleKey = currLeaf->leafKeys[splitLeafMaxInd1+1];
				printf("MIDDLE KEY: %d", middleKey);
			
			// Fill new leaf
			int p=0;
			for(p; p<splitLeafMaxInd2; p++){
				printf("buraya bakarlar <o>");
				newLeaf->leafKeys[p] = currLeaf->leafKeys[p+splitLeafMaxKeys1];
				newLeaf->datas[p] = currLeaf->datas[p+splitLeafMaxKeys1];
			}
			newLeaf->leafKeys[p] = lastKeyTemp;
			newLeaf->datas[p] = lastDataTemp;
			
			newLeaf->n = splitLeafMaxKeys2;
			
			if(isRootLeaf){
//				newLeaf->parentNode = newRoot;
			}else{
//				newLeaf->parentNode = currLeaf->parentNode;
			}
			
			printf("yeni yapragi doldurdum\n");
			
			// Delete old keys from splitted leaf
			for(int deleteInd = splitLeafMaxInd1+1; deleteInd<L-1; deleteInd++){
				currLeaf->leafKeys[deleteInd] = intNull[0];
				currLeaf->datas[deleteInd] = studentNull;
			}
			currLeaf->n = splitLeafMaxKeys1;
			currLeaf->isLeaf = true;
			
			printf("eski yapragi düzelttim\n");
			
			if(isRootLeaf){
				currLeaf->parentNode = newRoot;
			}
			
			// Prepare newRoot
			if(isRootLeaf){
				newRoot->parentNode = NULL;
				
				newRoot->n = 1;
				newRoot->keys[0] = middleKey;
				
				newRoot->childNodes[0] = currLeaf;
				newRoot->childNodes[1] = newLeaf;
				
				currLeaf->parentNode = newRoot;
				newLeaf->parentNode = newRoot;	
			}else{
				newLeaf->parentNode = currLeaf->parentNode;
			}
			
			// Point currLeaf's right
			currLeaf->right = newLeaf;
			
			if(isRootLeaf){
				// At the end, root points to newRoot now
				printf("root degistirildi\n");
				
				
				
				root = newRoot;
			}
		
			
			// Modify internal node
			
			printf("(Insert / BEFORE Modify Internal) : %s %s %d\n", currLeaf->isLeaf ? "true" : "false", !isRootLeaf ? "true" : "false", currLeaf->parentNode->n);

			struct BTreeNode * currInternalNode = currLeaf->parentNode;
					// DEBUG
					for(int t=0; t<currInternalNode->n; t++){
						printf("(Insert / Modify Internal (Before)) currInternalNode->keys[%d] : %d\n", t, currInternalNode->keys[t] );
					}
			
			
			
			if(!isRootLeaf){
				if(currLeaf->isLeaf && parNode->n < M-1){
					// if the parent internal node has space for a new key
					int currIndex3 = 0;
					
					
					printf("Curr Internal Node N = %d", currInternalNode->n);
					
					// check if the Middle key is lower than a key and put it between
					for(currIndex3; currIndex3< currInternalNode->n; currIndex3++){
						
						printf("\n (Insert / Modify Internal) middleKey %d, curr key %d\n", middleKey, currInternalNode->keys[currIndex3]);
						
						if(middleKey < currInternalNode->keys[currIndex3]){
							// Push other values while adding new Middle key to correct index
							printf("Yeni Middle key \n");
							maxFilledInd = currInternalNode->n;
							
							for(maxFilledInd; currIndex3<=maxFilledInd; maxFilledInd--){
								currInternalNode->keys[maxFilledInd+1] = currInternalNode->keys[maxFilledInd];
								currInternalNode->childNodes[maxFilledInd+1] = currInternalNode->childNodes[maxFilledInd];
							}
							
							currInternalNode->keys[currIndex3] = middleKey;
							currInternalNode->childNodes[currIndex3] = currLeaf;
							currInternalNode->childNodes[currIndex3+1] = newLeaf;
							
							currInternalNode->n = currInternalNode->n + 1;
							
							// DEBUG
							for(int t=0; t<currInternalNode->n; t++){
								printf("(Insert / Modify Internal) currInternalNode->keys[%d] : %d\n", t, currInternalNode->keys[t] );
							}
							
												// be sure that the internal nodes point to their right nodes
//					for(int y=0; y<currInternalNode->n-1; y++){
//						currInternalNode->childNodes[y]->right = currInternalNode->childNodes[y+1];
//					}	
							
							return root;
							
						}
						
					}
					
					// if it is the biggest key, add it to end
					
					int newLastIndex = currInternalNode->n;
					
					currInternalNode->keys[newLastIndex] = middleKey;
					currInternalNode->childNodes[newLastIndex] = currLeaf;
					currInternalNode->childNodes[newLastIndex+1] = newLeaf;
	
					// Increase number of keys
					currInternalNode->n = currInternalNode->n + 1;
					
					printf("Internal Node duzenlendi. N: %d \n", currInternalNode->n);
					
					// DEBUG
					for(int t=0; t<currInternalNode->n; t++){
						printf("(Insert / Modify Internal) currInternalNode->keys[%d] : %d\n", t, currInternalNode->keys[t] );
					}
					
					return root;
					
				}else{
					
					// parent internal node is full
					printf("\n Parent Node DOLU! \n");
					
					root = splitInternalNode( root, currInternalNode, middleKey, currLeaf, newLeaf, false);
					printf("\n Parent Node u ikiye ayirdim \n");
					
					printBplusTree(root);
					
					return root;
				}				
			}else{
				return root;
			}
			
			

			


			
			// DEBUG
			
			if(isRootLeaf){
				printf("Root ---------  (%s) \n", root->isLeaf ? "true":"false");
				for(int k=0; k < root->n; k++){
					printf("(%d)\n", root->keys[k]);
				}				
			}
			
			struct BTreeNode * leftNode = currLeaf;
			printf("LeftChild (%d)---------\n", leftNode->n);
			for(int k=0; k < leftNode->n; k++){
				printf("%d . Ogrenci isim-no: %s %d \n", k+1, leftNode->datas[k]->name, leftNode->datas[k]->studentNum);
			}
			
			struct BTreeNode * rightNode = newLeaf;
			printf("RightChild---------\n");
			for(int k=0; k< rightNode->n; k++){
				printf("%d . Ogrenci isim-no: %s %d \n", k+1, rightNode->datas[k]->name, rightNode->datas[k]->studentNum);
			}
			
		
			
//			printf("parentNode %d adet key iceriyor.\n", parNode->n);
//			for(int p=0; p<parNode->n; p++){
//				printf("parr key %d: %d\n", p, parNode->keys[p]);
//			}
			
			if(parNode && parNode->n == M-1){
				printf("\n Parent Node ilk kez doldu! \n");
			}
			

		
		
		// leaf full END
	}
	
	
	
}

void listTakenManyTimes(){
	
	// VYS 1'den fazla kez alanlar
	
	struct BTreeNode * firstLeaf = findLowestLeaf(BTreeALL);
	
	struct BTreeNode * currLeaf = firstLeaf;
	
	while(currLeaf->right){
		
		for(int p=0; p<currLeaf->n; p++){
			if(currLeaf->datas[p]->takenXTimes > 1){
		
				ManyTimesVYS_BTree = InsertStudent(ManyTimesVYS_BTree, currLeaf->datas[p]);
			}
		}
		
		currLeaf = currLeaf->right;
	}
	
		for(int p=0; p<currLeaf->n; p++){
			if(currLeaf->datas[p]->takenXTimes > 1){
		
				ManyTimesVYS_BTree = InsertStudent(ManyTimesVYS_BTree, currLeaf->datas[p]);
			}
		}
		
		currLeaf = currLeaf->right;	
	
	// BP 1'den fazla alanlar
	
	struct Node * firstNode = ALLhead;
	
	struct Node * currNode = firstNode;
	
	while(currNode->next){
		if(currNode->takenXTimes > 1){
			
			ManyTimesBP_LL = InsertAtTail(ManyTimesBP_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
			
		}
		
		currNode = currNode->next;
	}
	
		// son node'u da kontrol et
		if(currNode->takenXTimes > 1){
				
			ManyTimesBP_LL = InsertAtTail(ManyTimesBP_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
		}
	
	// 2 listeyi karþýlaþtýr ve ortaklarý bul
	
	firstNode = ManyTimesBP_LL;
	
	currNode = firstNode;
	
	while(currNode->next){
		if( isStudentRecorded( ManyTimesVYS_BTree, currNode->studentNum) ){
			
			ManyTimesCommon_LL = InsertAtTail(ManyTimesCommon_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
			
		}
		
		currNode = currNode->next;
	}
	
		// son node'u da kontrol et
		if( isStudentRecorded( ManyTimesVYS_BTree, currNode->studentNum) ){
			
			ManyTimesCommon_LL = InsertAtTail(ManyTimesCommon_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
			
		}
	
}

void StudentsOnlyBP(){
	
	// Sadece BP okuyanlarý bul
	
	struct Node * firstNode = ALLhead;
	
	struct Node * currNode = firstNode;
	
	while(currNode->next){
		if( ! isStudentRecorded( BTreeALL, currNode->studentNum) ){
			
			OnlyBP_LL = InsertAtTail(OnlyBP_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
			
		}
		
		currNode = currNode->next;
	}
	
	// son node'u da kontrol et
	if( ! isStudentRecorded( BTreeALL, currNode->studentNum) ){
		
		OnlyBP_LL = InsertAtTail(OnlyBP_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
		
	}	
	
}

void SplitVYS(){
	
	// VYS yi A-K ve L-Z þeklinde ayýr
	
	struct BTreeNode * firstLeaf = findLowestLeaf(BTreeALL);
	
	struct BTreeNode * currLeaf = firstLeaf;
	
	char charL = 'l';
	
	while(currLeaf->right){
		
		
		for(int p=0; p<currLeaf->n; p++){
			printf("surname first char: %c", currLeaf->datas[p]->surname[p] );
			if(currLeaf->datas[p]->surname[p] < charL ){
				// A-K
	
				BTreeAK =  InsertStudent(BTreeAK, currLeaf->datas[p]);

			}else{
				// L-Z
				
				BTreeLZ =  InsertStudent(BTreeLZ, currLeaf->datas[p]);
				
			}
		}
		
		currLeaf = currLeaf->right;
	}
		
		// son leaf de kontrol edilsin

		for(int p=0; p<currLeaf->n; p++){
			printf("surname first char: %c", currLeaf->datas[p]->surname[p] );
			if(currLeaf->datas[p]->surname[p] < charL ){
				// A-K
	
				BTreeAK =  InsertStudent(BTreeAK, currLeaf->datas[p]);

			}else{
				// L-Z
				
				BTreeLZ =  InsertStudent(BTreeLZ, currLeaf->datas[p]);
				
			}
		}
		

	
}

void StudentsBothButDifferentDepartment(){
	
	// Ýki dersi de alan ama BM harici bölüm okuyanlarý bul
	
	struct Node * firstNode = ALLhead;
	
	struct Node * currNode = firstNode;
	
	while(currNode->next){
		if( isStudentRecorded( BTreeALL, currNode->studentNum) ){
			
			if(currNode->department != "BM"){
				bothButNotBM_LL = InsertAtTail(bothButNotBM_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
			}
			
		}
		
		currNode = currNode->next;
	}
	
	// son node'u da kontrol et
		if( isStudentRecorded( BTreeALL, currNode->studentNum) ){
			
			if(currNode->department != "BM"){
				bothButNotBM_LL = InsertAtTail(bothButNotBM_LL, currNode->name, currNode->surname, currNode->department, currNode->studentNum, currNode->takenXTimes);
			}
			
		}
}

void printMenu(){
	int menuSecim = 0;
	while(true){
		printf("----- Menu ------\n");
		printf("1- a. Her iki dersi de birden fazla kez alan öðrencileri listele\n");
		printf("2- b. Sadece Bilgisayar Programlama dersini alan öðrencileri listele\n");
		printf("3- c1. Bilgisayar Programlama A ve B grubu olarak ikiye ayýr\n");
		printf("4- c2. Veritabaný Yönetim Sistemleri dersini iki gruba ayýr\n");
		printf("5- d. Bilgisayar Programlama ve Veritabaný Yönetim Sistemleri derslerinin her ikisini de alan ve baþka bölüm öðrencileri olan öðrencileri listele\n");
		printf("--- Sýralamalar ---\n");
		printf("6- a. Hýzlý sýralama algoritmasýný kullanarak her iki dersi de alan öðrencileri soyadlarýna göre sýrala\n");
		printf("7- b. Radiks sýralama algoritmasýný kullanarak sadece Veritabaný Yönetim Sistemleri dersini alan öðrencileri adlarýna göre sýrala\n");
		printf("8- c. Kümeleme sýralama algoritmasýný kullanarak Bilgisayar Programlama dersini alanlarý numaralarýna göre sýrala\n");
		
		scanf("%d", &menuSecim);
		
		switch(menuSecim){
			case 1: 
				
				listTakenManyTimes();
				printf("----- her 2 dersi 1'den fazla kez alanlar ---- start ---- \n");
				PrintOriginal(ManyTimesCommon_LL);
				printf("----- her 2 dersi 1'den fazla kez alanlar ---- end ---- \n");
				break;
			case 2: 
			
				StudentsOnlyBP();
				printf("----- sadece BP alanlar ---- start ---- \n");
				PrintOriginal(OnlyBP_LL);
				printf("----- sadece BP alanlar ---- end ---- \n");
				
				break;
			case 3:
				
				SplitList(ALLhead);
				
				printf("----- BP A-K ---- \n");
				PrintAK();
				printf("----- BP L-Z ---- \n");
				PrintLZ();
				
				break;
			case 4:
				
				SplitVYS();
				
				printf("----- VYS A-K ---- \n");
				printBplusTree(BTreeAK);
				printf("----- VYS L-Z ---- \n");
				printBplusTree(BTreeLZ);
				
				break;
			case 5:
				
				StudentsBothButDifferentDepartment();
				
				printf("----- 2 dersi de alip BM okumayanlar ---- start ---- \n");
				PrintOriginal(bothButNotBM_LL);
				printf("----- 2 dersi de alip BM okumayanlar ---- end ---- \n");
				 
				break;
			case 6:
				break;
			case 7:
				break;
			case 8:
				break;
		}
		
		printf("----- Islem Basariyla Tamamlandý ------\n");
		
	}
}


int main() {
	
//	nullPointer = NULL;

//	struct BTreeNode initialRoot;
//	initialRoot.n = 0;
//
//	root = &initialRoot;

	// Ýkili Liste
	ALLhead = NULL;
	ManyTimesBP_LL = NULL;
	ManyTimesCommon_LL = NULL;
	OnlyBP_LL = NULL;
	bothButNotBM_LL = NULL;

	// B+ Tree
	studentNull = NULL;
	nodeNull = NULL;

//	root = NULL;

//	root = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
//	root->n = 0;
//	root->isLeaf = true;
//	root->parentNode = NULL;
	
//	BTreeALL = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
//	BTreeALL->n = 0;
//	BTreeALL->isLeaf = true;
//	BTreeALL->parentNode = NULL;
//	BTreeALL->right = NULL;

	
	
//	BTreeAK = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
//	BTreeAK->n = 0;
//	BTreeAK->isLeaf = true;
//	BTreeAK->parentNode = NULL;
//	BTreeAK->right = NULL;
//	
//	BTreeLZ = (struct BTreeNode*) malloc(sizeof(struct BTreeNode));
//	BTreeLZ->n = 0;
//	BTreeLZ->isLeaf = true;
//	BTreeLZ->parentNode = NULL;
//	BTreeLZ->right = NULL;
	
	BTreeALL = CreateBTreeRoot(BTreeALL);
	BTreeAK = CreateBTreeRoot(BTreeAK);
	BTreeLZ = CreateBTreeRoot(BTreeLZ);
	
	ManyTimesVYS_BTree = CreateBTreeRoot(ManyTimesVYS_BTree);
	
		
//	printf("\nRoot n: %d\n", root->n);
	
	char names[40][40] = {"alper", "ahmet", "kaan", "arda", "fatma", "emine", "hatice", "zeynep", "ali", "hasan", "ibrahim", "ismail", "osman", "yusuf", "murat", "ramazan", "asu", "sude", "asya", "deniz", "aydin", "guven", "yilmaz", "yildirim", "ozturk" , "ozdemir", "arslan", "dogan", "kilic", "cetin", "kara", "koc", "kurt", "ozkan", "simsek", "yagmur", "vural", "dal", "yigit", "kayacan"};	
//	char names[60][60] = {"Noah","Morgan","Justine","Elodia","Sarita","Shaunna","Blythe","Mallie","Gerda","Sonny", "Kandace", "Latricia","Margy","Kisha","Kyla","Kazuko","Stella","Marcelle",  "Flossie",  "Kyung",  "Erick",  "Omer", "Josie",  "Nova",  "Wilhemina",  "Jada",  "Louie",  "Roderick",  "Rex", "Daisy",  "Sonia",  "Kacey",  "Carola",  "Marianela",  "Charlette",  "Celeste",  "Mirtha",  "Cristine",  "Agueda",  "Agripina", "Winfred",  "Yolanda",  "Marc",  "Pennie",  "Luci",  "Elsie",  "Kendrick",  "Chu",  "Mickie",  "Adina"  };
	
	char surnames[40][40] = {"aydin","guven", "yilmaz", "yildirim", "ozturk" , "ozdemir", "arslan", "dogan", "kilic", "cetin", "kara", "koc", "kurt", "ozkan", "simsek", "yagmur", "vural", "dal", "yigit", "kayacan", "alper", "ahmet", "kaan", "arda", "fatma", "emine", "hatice", "zeynep", "ali", "hasan", "ibrahim", "ismail", "osman", "yusuf", "murat", "ramazan", "asu", "sude", "asya", "deniz"};
	
	//	int studentNums[20] = {101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120};
	
//	int studentNums[20] = {106,107,102,101,108,109,104,105,103,110,111,112,113,114,115,116,117,118,119,120};
	
	int studentNums[60] = {101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160};
	
	char departments[10][10] = {"BM", "EM", "BESYO"};
	
//	root = BTreeAK;	
//	for(int i=0; i<5; i++){
////		struct Student newStudentX = { names[i], surnames[i], "BM", studentNums[i], 1};
//
//
//		struct Student * newStudentX1;
//		newStudentX1 = (struct Student*) malloc(sizeof(struct Student));
//
//		strcpy(newStudentX1->name, names[i]);
//		strcpy(newStudentX1->surname, surnames[i]);
//		strcpy(newStudentX1->department, "BM");
//		newStudentX1->studentNum =  studentNums[i];
//		newStudentX1->takenXTimes = 1;
//		
//		struct Student * insertResultX =  InsertStudent(newStudentX1);
//
//	};
//	printBplusTree();	
	
	////////////////////////////////
	// Create lists for both lessons
	////////////////////////////////
	
	int progCount = 0; 
	
	char charL = 'l';
	
	for(int i=0; i<31; i++){


		int departmentX1 = rand() % 3;

		if(i%2==0){
			struct Student * newStudentX1;
			newStudentX1 = (struct Student*) malloc(sizeof(struct Student));
	
			strcpy(newStudentX1->name, names[i]);
			strcpy(newStudentX1->surname, surnames[i]);
			strcpy(newStudentX1->department, departments[departmentX1]);
			newStudentX1->studentNum =  studentNums[i];
			newStudentX1->takenXTimes = 1 + rand() % 2;
			
			BTreeALL = InsertStudent(BTreeALL, newStudentX1);			
		}
		

		
//		printf("surname first char: %c", newStudentX1->surname[0]);
		
//		if(newStudentX1->surname[0] < charL){
//			// A-K
//
//			BTreeAK =  InsertStudent(BTreeAK, newStudentX1);
//
//		}else{
//			// L-Z
//			
//			BTreeLZ =  InsertStudent(BTreeLZ, newStudentX1);
//			
//		}
		
//		printBplusTree(BTreeAK);
//		printBplusTree(BTreeLZ);

		if(i%3==0){
			
			if(!progCount==0){
				ALLhead = InsertAtTail(ALLhead, names[i], surnames[i], departments[departmentX1], studentNums[i], 1 + rand() % 2 );
			}else{
				ALLhead = InsertAtHead(ALLhead, names[i], surnames[i], departments[departmentX1], studentNums[i], 1 + rand() % 2 );
			}
			
			progCount++;
		}
		
	};
	


	
	printf("\n-----\nHi\n");
	
	
	
//	printBplusTree(BTreeAK);
//	
//	printBplusTree(BTreeLZ);

	printBplusTree(BTreeALL);

	PrintOriginal(ALLhead);
	
	printMenu();
	
	printf("-------- SON --------\n");
	
//	printf("Ogrenci Adlari search: %s | %s | %s", searchResult->name, searchResult2->name, searchResult3->name);
//	printf("Ogrenci Numaralari search: %d | %d | %d", searchResult->studentNum, searchResult2->studentNum, searchResult3->studentNum);


	return 0;
}
