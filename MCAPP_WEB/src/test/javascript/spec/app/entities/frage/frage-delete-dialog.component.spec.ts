/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, inject, fakeAsync, tick } from '@angular/core/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { JhiEventManager } from 'ng-jhipster';

import { McappWebTestModule } from '../../../test.module';
import { FrageDeleteDialogComponent } from 'app/entities/frage/frage-delete-dialog.component';
import { FrageService } from 'app/entities/frage/frage.service';

describe('Component Tests', () => {
    describe('Frage Management Delete Component', () => {
        let comp: FrageDeleteDialogComponent;
        let fixture: ComponentFixture<FrageDeleteDialogComponent>;
        let service: FrageService;
        let mockEventManager: any;
        let mockActiveModal: any;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [FrageDeleteDialogComponent]
            })
                .overrideTemplate(FrageDeleteDialogComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(FrageDeleteDialogComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(FrageService);
            mockEventManager = fixture.debugElement.injector.get(JhiEventManager);
            mockActiveModal = fixture.debugElement.injector.get(NgbActiveModal);
        });

        describe('confirmDelete', () => {
            it('Should call delete service on confirmDelete', inject(
                [],
                fakeAsync(() => {
                    // GIVEN
                    spyOn(service, 'delete').and.returnValue(of({}));

                    // WHEN
                    comp.confirmDelete(123);
                    tick();

                    // THEN
                    expect(service.delete).toHaveBeenCalledWith(123);
                    expect(mockActiveModal.dismissSpy).toHaveBeenCalled();
                    expect(mockEventManager.broadcastSpy).toHaveBeenCalled();
                })
            ));
        });
    });
});
