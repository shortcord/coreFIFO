#include <stdio.h>

int makeSock(const char*, int);
int deleteSock(const char*);
char* getLastError(void);

int main()
{
 if(makeSock("/tmp/test.sock", 0660))
 {
  printf("ERROR %s\n", getLastError());
  if(deleteSock("/dev/null"))
  {
   printf("ERROR %s\n", getLastError());
  }
 }
}
