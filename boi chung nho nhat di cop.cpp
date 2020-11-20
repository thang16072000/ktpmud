#include <iostream>
#include <math.h>
using namespace std;
int main(){
int a,b,c;
cin>>a>>b>>c;
if (a==0)
float x0=-c/b;
if (a!=0) {
	
	float delta=b*b-4*a*c;
		if (delta>0) { cout <<"x1= "<<(-b+sqrt(delta))/(2*a)<<"\t x2= "<<(-b-sqrt(delta))/(2*a);}
		else if (delta==0) {cout << "nghiem kep x1=x2= "<<-b/(2*a);		}
		else if (delta<0){cout<<"phuong trinh vo nghiem";}
		}
	
	
	
	return 0;
}
